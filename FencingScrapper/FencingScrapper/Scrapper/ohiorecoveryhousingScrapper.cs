using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    public class ohiorecoveryhousingScrapper : IScrapper
    {
        public void ExtractData()
        {
            string outhtml = Helper.GetHtmlFromUrl(GetUrl());
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//div[contains(@class, 'orhl_search_results')]//div[contains(@class, 'orhl_search_result')]");
            int i = 0;
            string title = "";
            string address = "";
            string phone = "";
            string companyURL = "";
            string name = "";
            List<scrapperModel> modelData = new List<scrapperModel>();
            foreach (var item in items)
            {

                scrapperModel model = new scrapperModel();
                HtmlNode titlenode = item.SelectSingleNode(".//div[contains(@class,'orhl_search_result_profile_title')]");
                if (titlenode != null)
                {
                    i++;
                    title = titlenode.InnerText.Replace("\n", " ").Replace("\r", " ");

                    HtmlNodeCollection contactnode = item.SelectNodes(".//div[contains(@class,'orhl_search_result_profile_contact')]//span[contains(@class,'orhl_search_result_profile_contact_row')]");
                    if (contactnode != null)
                    {
                        phone = contactnode[0].InnerText.Replace("\n", " ").Replace("\r", " ");
                        name = contactnode[1].InnerText.Replace("\n", " ").Replace("\r", " ");
                        HtmlNode anchortag = item.SelectSingleNode(".//div[contains(@class,'orhl_search_result_profile_contact')]//span[contains(@class,'orhl_search_result_profile_contact_row')]//a");
                        if (anchortag != null)
                        {
                            companyURL = anchortag.InnerText.Replace("\n", " ").Replace("\r", " ");
                        }
                    }
                    KeyValuePair<string, string> contactdetail = Helper.GetFirstAndLastName(name);
                    model.FirstName = contactdetail.Key;
                    model.LastName = contactdetail.Value;
                    model.CompanyName = title;
                    model.Phone = phone;
                    model.CompanyUrl = companyURL;
                    model.Url = "https://find.ohiorecoveryhousing.org";
                    modelData.Add(model);
                }
            }

            GenrateReport.StartGenerate("ohiorecoveryhousing", modelData);
        }

        public string GetUrl()
        {
            return "https://find.ohiorecoveryhousing.org/?orhl_sc_gender&orhl_sc_within=9999&orhl_sc_location&orhl_sc_NARR_level&orhl_search=Search%20Now#orhl_search_results";
        }
    }
}
