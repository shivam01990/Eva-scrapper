using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    public class naatpScrapper : IScrapper
    {
        private string GetUrl(int pageNo)
        {
            return GetUrl() + "?page=" + pageNo;
        }

        public string GetUrl()
        {
            return "https://www.naatp.org/resources/addiction-industry-directory";
        }

        public void ExtractData()
        {
            List<scrapperModel> modelData = new List<scrapperModel>();
            int totalPages = 18;
            for (int i = 0; i < totalPages; i++)
            {
              modelData.AddRange(GetData(i));
            }
        }

        private List<scrapperModel> GetData(int pageNo)
        {
            string outhtml = Helper.OpenIEURL(GetUrl(pageNo));
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//div[contains(@class, 'panel') and contains(@class, 'panel-default')]");
            List<scrapperModel> modelData = new List<scrapperModel>();
            for (int i = 0; i < items.Count; i++)
            {
                string title = "";
                string address = "";
                string phone = "";
                string companyURL = "";               

                HtmlNode titlenode = items[i].SelectSingleNode(".//div[contains(@class, 'panel-heading') and contains(@class, 'panel-heading2')]//h3");
                if (titlenode != null)
                {
                    title = titlenode.InnerText.Replace("\n", " ").Replace("\r", " ");
                }

                HtmlNodeCollection addressnode = items[i].SelectNodes(".//div[contains(@class, 'panel-body')]//a");
                if (addressnode != null)
                {
                    address = addressnode[0].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                    phone = addressnode[1].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                }


                scrapperModel model = new scrapperModel();
                KeyValuePair<string, string> cityandState = Helper.GetStateAndCity(address);
                model.City = cityandState.Key;
                model.State = cityandState.Value;
                model.CompanyName = title;
                model.Url = GetUrl();
                model.Address = address;
                model.Phone = phone;
                model.CompanyUrl = companyURL;
                modelData.Add(model);

            }

            return modelData;
        }       
    }
}
