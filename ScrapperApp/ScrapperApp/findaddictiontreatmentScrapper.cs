using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapperApp
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
            HtmlNodeCollection anchorStates = doc.DocumentNode.SelectNodes("(//div[contains(@class, 'well') and contains(@class, 'well-sm')]//div[@class='row phn'])[2]//ul//li//a");

        }
    }
}
