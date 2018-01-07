using HtmlAgilityPack;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FencingScrapper.Scrapper
{
    public class mkrecoverycoachingScrapper : IScrapper
    {
        public string GetUrl()
        {
            return "http://www.mkrecoverycoaching.com/recovery-coach-training-organizations/";
        }

        public void ExtractData()
        {

            string outhtml = Helper.OpenIEURL(GetUrl());
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//p");
            List<scrapperModel> modelData = new List<scrapperModel>();
            for (int i = 0; i < items.Count; i++)
            {
                string title = "";
                string address = "";

                HtmlNode anchortag = items[i].SelectSingleNode(".//a");

                title = items[i].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("&amp;", " ").Replace("&nbsp;", " ");
                if (anchortag != null)
                {
                    address = anchortag.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("&amp;", " ").Replace("&nbsp;", " ");

                    //HtmlNodeCollection addressnode = items[i].SelectNodes(".//div[contains(@class, 'panel-body')]//a");

                    //if (addressnode != null)
                    //{
                    //    address = addressnode[0].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                    //    phone = addressnode[1].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                    //}


                    scrapperModel model = new scrapperModel();
                    //KeyValuePair<string, string> cityandState = Helper.GetStateAndCity(address);

                    model.CompanyName = title;
                    model.Address = address;
                    model.SourceUrl = GetUrl();
                    modelData.Add(model);

                }

            }
            GenrateReport.StartGenerate("mkrecovery", modelData);
        }
    }
}
