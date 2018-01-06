using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    public class mashsoberhousingScrapper : IScrapper
    {
        public void ExtractData()
        {
            string outhtml = Helper.OpenIEURL(GetUrl());
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//table//tbody//tr");
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

                HtmlNodeCollection columns = item.SelectNodes(".//td");
                model.CompanyName = columns[0].InnerText.Replace("\n", " ").Replace("\r", " ");
                model.Address = columns[2].InnerText.Replace("\n", " ").Replace("\r", " ");
                KeyValuePair<string, string> cityandState = Helper.GetStateAndCity(model.Address);
                model.City = cityandState.Key;
                model.State = cityandState.Value;
                HtmlNode anchortag = item.SelectSingleNode(".//a");
                if (anchortag != null)
                {
                   address =anchortag.InnerText.Replace("\n", " ").Replace("\r", " ");
                    if (!address.Contains("@"))
                    {
                        model.CompanyUrl = address;
                    }
                    else
                    {
                        model.Email = address;
                    }                   
                }
                if (model.CompanyUrl != null)
                {
                    model.CompanyName = model.CompanyName.Replace(model.CompanyUrl, "");
                }

                HtmlNodeCollection Contacts = columns[5].SelectNodes(".//br");
                if (Contacts != null)
                {
                    if (Contacts.Count >= 1)
                    {
                        name = Contacts[0].PreviousSibling.InnerText;
                        KeyValuePair<string, string> contactdetail = Helper.GetFirstAndLastName(name);
                        model.FirstName = contactdetail.Key;
                        model.LastName = contactdetail.Value;
                    }

                    if (Contacts.Count >= 2)
                    {
                        model.Phone = Contacts[1].PreviousSibling.InnerText.Replace("\n", " ").Replace("\r", " "); ;
                    }

                    if (Contacts.Count >= 3)
                    {

                        address = Contacts[2].PreviousSibling.InnerText.Replace("\n", " ").Replace("\r", " ");
                        if (!address.Contains("@"))
                        {
                            model.CompanyUrl = address;
                        }
                        else
                        {
                            model.Email = address;
                        }
                    }
                }
                model.Url = GetUrl();
                modelData.Add(model);

            }
            GenrateReport.StartGenerate("mashsoberhousing-4", modelData);
        }

        public string GetUrl()
        {
            return "https://mashsoberhousing.org/certified-residences/";
        }
    }
}
