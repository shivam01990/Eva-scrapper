using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    public class farronlineScrapper : baseScrapper, IScrapper
    {
        List<scrapperModel> modelData;
        public farronlineScrapper()
        {
            modelData = new List<scrapperModel>();
            this.ScraperEventHandler += OnEventExecute;
        }

        private void OnEventExecute(object sender, EventArgs e)
        { 
            string outhtml = (string)sender;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
            HtmlNodeCollection rows = doc.DocumentNode.SelectNodes("//table[contains(@id, 'members')]//tr");
            for (int i = 2; i < rows.Count; i++)
            { 
                HtmlNodeCollection columns = rows[i].SelectNodes(".//td");
                if (columns != null)
                {
                    scrapperModel model = new scrapperModel();
                    model.SourceUrl = GetUrl();
                    model.CompanyName = columns[1].InnerText.Replace("\n", " ").Replace("\r", " ");

                    if (model.CompanyName.Trim() != "")
                    {
                        model.City = columns[2].InnerText.Replace("\n", " ").Replace("\r", " ");

                        string price = columns[6].InnerText.Replace("\n", " ").Replace("\r", " ");
                        if (price != "")
                        {
                            model.Prices = "$" + price;
                        }
                        HtmlNode anchortag = columns[7].SelectSingleNode(".//a");
                        if (anchortag != null)
                        {
                            model.DetailsPageUrl = anchortag.GetAttributeValue("href", string.Empty).Replace("\n", " ").Replace("\r", " ").Replace("&amp;", " ").Replace("&nbsp;", " ");
                        }

                        modelData.Add(model);
                    }
                }


            }
        }

        public void ExtractData()
        {
            MainWindow browser = new MainWindow(GetUrl(), this);
            browser.ShowDialog();
            GetSubPageData(modelData);
            GenrateReport.StartGenerate("farronline", modelData);
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
                            HtmlNode websiteNode = doc.DocumentNode.SelectSingleNode("//a[contains(@class, 'btn-mini-grey')]");
                            if(websiteNode!=null)
                            {
                                model.CompanyUrl = websiteNode.GetAttributeValue("href", string.Empty).Replace("\n", " ").Replace("\r", " ").Replace("&amp;", " ").Replace("&nbsp;", " ");

                            }
                            HtmlNodeCollection tableNode = doc.DocumentNode.SelectNodes("//table[contains(@class, 'plain-border')]");
                            HtmlNodeCollection contactNode = tableNode[2].SelectNodes(".//tr//td");
                            if (contactNode != null)
                            {
                                if (contactNode.Count >= 1)
                                {
                                    string name = contactNode[0].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ").Replace("Address", "").Replace("                                                                           ", "").Replace("                        ", "");
                                    KeyValuePair<string, string> namepair = Helper.GetFirstAndLastName(name);
                                    model.FirstName = namepair.Key;
                                    model.LastName = namepair.Value;
                                }

                                if (contactNode.Count >= 2)
                                {
                                    model.Phone = contactNode[1].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ").Replace("Address", "").Replace("                                                                           ", "").Replace("                        ", "");
                                }

                                if (contactNode.Count >= 3)
                                {
                                    model.Email = contactNode[2].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ").Replace("Address", "").Replace("                                                                           ", "").Replace("                        ", "");
                                }
                            }
                        }
                        catch { }



                        try
                        {

                            HtmlNodeCollection websitenodes = doc.DocumentNode.SelectNodes("//div[contains(@id, 'detail_right')]//a");
                            if (websitenodes != null)
                            {
                                foreach (var website in websitenodes)
                                {
                                    string websiteurl = website.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                                    if (websiteurl.Contains("www."))
                                    {
                                        model.CompanyUrl = websiteurl;
                                    }
                                }
                            }
                        }
                        catch
                        { }




                    }
                }
                catch { Helper.AddtoLogFile("Error in" + model.DetailsPageUrl); }
            }
        }

        public string GetUrl()
        {
            return "http://farronline.org/certification/certified-residences";
        }
    }
}
