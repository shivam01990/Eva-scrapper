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
            string outhtml = Helper.GetHtmlFromUrl(GetUrl(pageNo));
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//table[contains(@class, 'views-table') and contains(@class, 'cols-3')]//tr");
            List<scrapperModel> modelData = new List<scrapperModel>();
            for (int i = 0; i < items.Count; i++)
            {
                string title = "";
                string address = "";
                string phone = "";
                string companyURL = "";

                HtmlNodeCollection columns = items[i].SelectNodes(".//td");
                if (columns != null)
                {
                    title = columns[0].InnerText.Replace("\n", " ").Replace("\r", " ");
                    address = columns[1].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
               
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
            }

            return modelData;
        }
    }
}
