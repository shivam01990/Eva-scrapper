using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    public class recoverycompassScrapper : baseScrapper, IScrapper
    {
        List<scrapperModel> modelData;

        public recoverycompassScrapper()
        {
            modelData = new List<scrapperModel>();
            this.ScraperEventHandler += OnEventExecute;
        }

        private void OnEventExecute(object sender, EventArgs e)
        {
            string outhtml = (string)sender;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//ul[contains(@class, 'job_listings')]//li");
            foreach (var item in items)
            {
                scrapperModel model = new scrapperModel();

                HtmlNode anchortag = item.SelectSingleNode(".//div[contains(@class, 'content-box')]//a");

                if (anchortag != null)
                {
                    try
                    {
                        model.DetailsPageUrl = anchortag.GetAttributeValue("href", string.Empty).Replace("\n", " ").Replace("\r", " ").Replace("&amp;", " ").Replace("&nbsp;", " ");                     
                    }
                    catch { }
                }

                modelData.Add(model);
            }
        }

        public void ExtractData()
        {
            //MainWindow browser = new MainWindow(GetUrl(), this);
            ChromeBrowserWindow browser = new ChromeBrowserWindow(GetUrl(), this);
            browser.ShowDialog();
            GetSubPageData(modelData);
            GenrateReport.StartGenerate("recoverycompass", modelData);
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
                            HtmlNode titlenode = doc.DocumentNode.SelectSingleNode("//h1[contains(@class, 'job_listing-title')]");
                            if (titlenode != null)
                            {
                                model.CompanyName = titlenode.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch { }

                        try
                        {
                            HtmlNode phone = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'job_listing-phone')]");
                            if (phone != null)
                            {
                                model.Phone = phone.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("CALL OR TEXT:", " ");
                            }
                        }
                        catch { }

                        try
                        {

                            HtmlNode addressnode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'job_listing-location-formatted')]");
                            if (addressnode != null)
                            {
                                model.Address = addressnode.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                                KeyValuePair<string, string> citynstate = Helper.GetStateAndCity(model.Address);
                                model.City = citynstate.Key;
                                model.State = citynstate.Value;
                            }
                        }
                        catch
                        { }


                        try
                        {
                            HtmlNode website = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'job_listing-url')]");
                            if (website != null)
                            {
                                model.CompanyUrl = website.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch { }

                        try
                        {

                            HtmlNode pricenode = doc.DocumentNode.SelectSingleNode("//div[contains(@id, 'jmfe-custom-price')]");
                            if (pricenode != null)
                            {
                                model.Prices = pricenode.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                               
                            }
                        }
                        catch
                        { }

                        model.IsDetailsPageScrapped = true;
                        //ScrapperProvider.SaveScrapper(model);

                    }
                }
                catch { Helper.AddtoLogFile("Error in" + model.DetailsPageUrl); }
            }
        }


        public string GetUrl()
        {
            return "http://www.recoverycompass.com";
        }
    }
}
