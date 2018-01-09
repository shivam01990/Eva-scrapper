using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    class soberhousing : IScrapper
    {
        private string GetUrl(int pageNo)
        {
            return GetUrl() + "?page=" + pageNo;
        }

        public string GetUrl()
        {
            return "https://soberhousing.net/search-homes/";
        }

        public void ExtractData()
        {
            List<scrapperModel> modelData = new List<scrapperModel>();
            string outhtml = Helper.GetHtmlFromUrl(GetUrl());
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//article");
            for (int i = 0; i < items.Count; i++)
            {
                string address = "";
                scrapperModel model = new scrapperModel();
                HtmlNodeCollection columns = items[i].SelectNodes(".//h2//a");
                HtmlNodeCollection columns2 = items[i].SelectNodes(".//div[contains(@class, 'sln-house-detail one-third first')]");
                HtmlNodeCollection columns3 = items[i].SelectNodes(".//div[contains(@class, 'sln-house-detail one-third right-align')]");
                HtmlNodeCollection columns4 = items[i].SelectNodes(".//div[contains(@class, 'sln-house-detail one-half first')]");
                HtmlNodeCollection columns5 = items[i].SelectNodes(".//div[contains(@class, 'sln-house-detail one-half')]");
                HtmlNodeCollection columns6 = items[i].SelectNodes(".//div[contains(@class, 'sln-house-detail first')]//a");
                if (columns != null)
                {
                    model.CompanyName = columns[0].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&#8211;", "");
                }
                if (columns2 != null)
                {
                    string jj = columns2[0].InnerText.ToString();
                    string[] test = jj.Split(new string[] { "Location:" }, StringSplitOptions.None);
                    if (test.Length > 0)
                    {
                        model.City = test[1].ToString();
                    }
                }
                if (columns3 != null)
                {
                    model.Prices = columns3[0].InnerText.ToString();
                }
                if (columns4 != null)
                {
                    string jj = columns4[0].InnerText.ToString();
                    string[] test = jj.Split(new string[] { "Contact: " }, StringSplitOptions.None).Last().Split(' ');
                    model.FirstName = test.First();
                    if (test.Length > 1)
                    {
                        model.LastName = test[1].ToString();
                    }

                }
                if (columns5 != null)
                {
                    string jj = columns5[1].InnerText.ToString();
                    if (jj.Length > 0)
                    {
                        string[] test = jj.Split(new string[] { "Phone:" }, StringSplitOptions.None);
                        if (test.Length > 0)
                        {
                            model.Phone = test[1].ToString();
                        }
                    }
                }
                if (columns6 != null)
                {
                    model.CompanyUrl= columns6[0].InnerText.ToString();
                }
                    model.SourceUrl = GetUrl();
                    modelData.Add(model);
            }
            GenrateReport.StartGenerate("sober", modelData);
        }
    }
}
