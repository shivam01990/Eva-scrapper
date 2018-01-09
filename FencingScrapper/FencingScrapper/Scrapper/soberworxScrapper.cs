using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    public class soberworxScrapper : baseScrapper, IScrapper
    {
        List<scrapperModel> modelData;

        public soberworxScrapper()
        {
            modelData = new List<scrapperModel>();
            this.ScraperEventHandler += OnEventExecute;
        }

        private void OnEventExecute(object sender, EventArgs e)
        {
            string outhtml = (string)sender;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
            HtmlNodeCollection rows = doc.DocumentNode.SelectNodes("//ul[contains(@class, 'job_listings')]//li");
            foreach (var item in rows)
            {
                scrapperModel model = new scrapperModel();
                model.SourceUrl = GetUrl();
                HtmlNode anchortag = item.SelectSingleNode(".//div[contains(@class, 'content-box')]//a");
                if (anchortag != null)
                {
                    model.DetailsPageUrl = anchortag.GetAttributeValue("href", string.Empty).Replace("\n", " ").Replace("\r", " ").Replace("&amp;", " ").Replace("&nbsp;", " ");

                }

                HtmlNode titlenode = item.SelectSingleNode(".//div[contains(@class, 'content-box')]//h2[@class='job_listing-title']");
                if (titlenode != null)
                {
                    model.CompanyName = titlenode.InnerText.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("&amp;", " ").Replace("&nbsp;", " ");

                }

                HtmlNode addressnode = item.SelectSingleNode(".//div[contains(@class, 'content-box')]//div[contains(@class, 'job_listing-location-formatted')]//a");
                if (addressnode != null)
                {
                    model.Address = addressnode.InnerText.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("&amp;", " ").Replace("&nbsp;", " ");
                    KeyValuePair<string, string> citynstate = Helper.GetStateAndCity(model.Address);
                    model.City = citynstate.Key;
                    model.State = citynstate.Value;
                }

                HtmlNode phonenode = item.SelectSingleNode(".//div[contains(@class, 'content-box')]//div[contains(@class, 'job_listing-phone')]");
                if (phonenode != null)
                {
                    model.Phone = phonenode.InnerText.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("&amp;", " ").Replace("&nbsp;", " ");                    
                }

                modelData.Add(model);
            }
        }


        public void ExtractData()
        {
            ChromeBrowserWindow browser = new ChromeBrowserWindow(GetUrl(), this);
            browser.ShowDialog();
            GetSubPageData(modelData);
            GenrateReport.StartGenerate("soberworx", modelData);
        }

        public string GetUrl()
        {
            return "https://soberworx.com";
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
                            HtmlNode website = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'job_listing-url')]");
                            if (website != null)
                            {
                                model.CompanyUrl = website.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch { }

                        

                    }
                }
                catch { Helper.AddtoLogFile("Error in" + model.DetailsPageUrl); }
            }
        }
    }
}
