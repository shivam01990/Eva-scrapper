using HtmlAgilityPack;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    public class findaddictiontreatmentScrapper : IScrapper
    {
        public string GetUrl()
        {
            return "https://findaddictiontreatment.ny.gov/";
        }

        public void ExtractData()
        {
            string outhtml = Helper.OpenIEURL(GetUrl());
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//div[contains(@class, 'panel') and contains(@class, 'panel-default')]");
            List<scrapperModel> modelData = new List<scrapperModel>();
            for (int i = 0; i < items.Count; i++)
            {
                string title = "";
                string address = "";
                string phone = "";
                string state = "";
                string city = "";
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
                modelData.Add(model);


            }
            GenrateReport.StartGenerate("findaddiction", modelData);

        }
    }
}
