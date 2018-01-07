using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    public class sobernexusScrapper : baseScrapper, IScrapper
    {
        List<scrapperModel> modelData;

        public sobernexusScrapper()
        {
            modelData = new List<scrapperModel>();
            this.ScraperEventHandler += OnEventExecute;
        }

        private void OnEventExecute(object sender, EventArgs e)
        {
            string outhtml = (string)sender;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
            HtmlNodeCollection rows = doc.DocumentNode.SelectSingleNode("//div[contains(@id, 'results-container')]").ChildNodes;

            foreach (var item in rows)
            {
                if (item.Name == "div")
                {
                    scrapperModel model = new scrapperModel();
                    model.SourceUrl = GetUrl();

                    HtmlNode companynamenode = item.SelectSingleNode((".//h3[contains(@class, 'business-name')]//span[2]"));
                    if (companynamenode != null)
                    {
                        model.CompanyName = companynamenode.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Trim();

                    }

                    HtmlNode addressnode = item.SelectSingleNode((".//span[contains(@class, 'address')]"));
                    if (addressnode != null)
                    {
                        model.Address = addressnode.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Trim();
                        KeyValuePair<string, string> citynstate = Helper.GetStateAndCity(model.Address);
                        model.City = citynstate.Key;
                        model.State = citynstate.Value;
                    }

                    HtmlNode phonenode = item.SelectSingleNode((".//span[contains(@class, 'phone')]"));
                    if (phonenode != null)
                    {
                        model.Phone = phonenode.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Trim();
                       
                    }

                    HtmlNode websitenode = item.SelectSingleNode((".//ul[contains(@class, 'company-features')]//li//a"));
                    if (websitenode != null)
                    {
                        model.CompanyUrl = websitenode.GetAttributeValue("href",string.Empty).Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Trim();
                    }

                    modelData.Add(model);
                }
            }
        }

        public void ExtractData()
        {
            MainWindow browser = new MainWindow(GetUrl(), this);
            browser.ShowDialog();
            GenrateReport.StartGenerate("sobernexus", modelData);
        }

        public string GetUrl()
        {
            return "https://www.sobernexus.com/search";
        }
    }
}
