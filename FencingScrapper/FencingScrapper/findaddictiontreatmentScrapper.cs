using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper
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

            for (int i = 0; i < items.Count; i++)
            {
                HtmlNode titlenode = items[i].SelectSingleNode(".//div[contains(@class, 'panel-heading') and contains(@class, 'panel-heading2')]//h3");
                if (titlenode != null)
                {
                    string title = titlenode.InnerText;
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                HtmlNodeCollection addressnode = items[i].SelectNodes(".//div[contains(@class, 'panel-body')]//a");
                if (addressnode != null)
                {
                    string address = addressnode[0].InnerText;
                    string phone= addressnode[1].InnerText;
                }
            }
        }
    }
}
