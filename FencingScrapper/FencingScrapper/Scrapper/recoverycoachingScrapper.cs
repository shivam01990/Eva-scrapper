using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace FencingScrapper.Scrapper
{
    public class recoverycoachingScrapper : baseScrapper, IScrapper
    {
        List<scrapperModel> modelData;
        public recoverycoachingScrapper()
        {
            modelData = new List<scrapperModel>();
            this.ScraperEventHandler += OnEventExecute;
        }

        private void OnEventExecute(object sender, EventArgs e)
        {
            string outhtml = (string)sender;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
            HtmlNodeCollection rows = doc.DocumentNode.SelectNodes("//div[contains(@id, 'people')]//div[contains(@class, 'person')]");
            for (int i = 0; i < rows.Count; i++)
            {
                
                HtmlNode anchortag = rows[i].SelectSingleNode(".//a[contains(@class, 'member-link')]");
                if (anchortag != null)
                {
                    scrapperModel model = new scrapperModel();
                    model.SourceUrl = GetUrl();


                    if (anchortag != null)
                    {
                        string name = anchortag.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                      KeyValuePair<string,string> names=  Helper.GetFirstAndLastName(name);
                        model.FirstName = names.Key;
                        model.LastName = names.Value;
                        string link = anchortag.GetAttributeValue("href", string.Empty).Replace("\n", " ").Replace("\r", " ").Replace("&amp;", "&").Replace("&nbsp;", "");
                        link = link.Replace("http://www.recoverycoaching.org", "");
                        model.DetailsPageUrl = "http://www.recoverycoaching.org" + link;
                    }

                    modelData.Add(model);
                }

               
            }
        }

        public void ExtractData()
        {
            MainWindow browser = new MainWindow(GetUrl(), this);
            browser.ShowDialog();
            GetSubPageData(modelData);
            GenrateReport.StartGenerate("recoverycoaching", modelData);
        }

        private void GetSubPageData(List<scrapperModel> modeldata)
        {
            foreach (var model in modeldata)
            {
                try
                {
                    if (model.DetailsPageUrl != string.Empty)
                    {
                        Console.WriteLine("Grabbing data for suburl" + model.DetailsPageUrl);
                        string outhtml = Helper.GetHtmlFromUrl(model.DetailsPageUrl);
                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(outhtml);


                        try
                        {
                            HtmlNode phone = doc.DocumentNode.SelectSingleNode("//div[contains(@id, 'ctl00_ctl00_membership_bio_info_standard_panel_phone_panel')]//div[2]");
                            if (phone != null)
                            {
                                model.Phone = phone.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch { }

                        //try
                        //{

                        //    HtmlNode email = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'email')]");
                        //    if (email != null)
                        //    {
                        //        model.Email = email.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                        //    }
                        //}
                        //catch
                        //{ }

                        try
                        {
                            HtmlNode website = doc.DocumentNode.SelectSingleNode("//div[contains(@id, 'ctl00_ctl00_membership_bio_info_standard_panel_website_panel')]//div[2]"); ;
                            if (website != null)
                            {
                                model.CompanyUrl = website.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch { }

                        try
                        {
                            HtmlNode comanynamenoad = doc.DocumentNode.SelectSingleNode("//div[contains(@id, 'ctl00_ctl00_membership_bio_info_standard_panel_phone_panel')]").NextSibling.NextSibling.NextSibling.NextSibling;
                            if (comanynamenoad != null)
                            {
                                model.CompanyName = comanynamenoad.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch { }

                       

                        try
                        {
                            HtmlNode addressnode = doc.DocumentNode.SelectSingleNode("//div[contains(@id, 'ctl00_ctl00_membership_bio_info_standard_panel_title_panel')]").NextSibling;
                            HtmlNodeCollection addresscoll = addressnode.SelectNodes(".//following-sibling::div[@class='content-text']");
                            model.Address = "";
                            foreach (var node1 in addresscoll)
                            {
                                model.Address+= node1.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ").Replace("&nbsp;", " ")+" ";
                            }

                            KeyValuePair<string, string> citynstate = Helper.GetStateAndCity(model.Address);
                            model.City = citynstate.Key.Trim().Split(' ').LastOrDefault();
                            model.State = citynstate.Value.Trim().Split(' ').FirstOrDefault();
                          
                        }
                        catch { }


                        model.IsDetailsPageScrapped = true;
                        //ScrapperProvider.SaveScrapper(model);

                    }
                }
                catch { Helper.AddtoLogFile("Error in" + model.DetailsPageUrl); }
            }
        }

        public string GetUrl()
        {
            return "http://www.recoverycoaching.org/content.aspx?page_id=78&club_id=263697";
        }
    }
}
