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
            for (int i = 0; i <= totalPages; i++)
            {
                Console.WriteLine("Grabbing data for " + GetUrl(i));
                try
                {
                    modelData.AddRange(GetData(i));
                }
                catch { Helper.AddtoLogFile("Error in" + GetUrl(i)); }
            }

            GetSubPageData(modelData);
            GenrateReport.StartGenerate("naatp", modelData);
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
                    HtmlNode anchortag = columns[0].SelectSingleNode(".//a");

                    address = columns[1].InnerText.Replace(", United States", "").Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");

                    scrapperModel model = new scrapperModel();

                    if (anchortag != null)
                    {
                        model.DetailsPageUrl = anchortag.GetAttributeValue("href", string.Empty).Replace("\n", " ").Replace("\r", " ").Replace("&amp;", " ").Replace("&nbsp;", " ");
                        if (model.DetailsPageUrl != string.Empty)
                        {
                            model.DetailsPageUrl = "https://www.naatp.org" + model.DetailsPageUrl;
                        }
                    }
                    KeyValuePair<string, string> cityandState = Helper.GetStateAndCity(address);
                    model.City = cityandState.Key;
                    model.State = cityandState.Value;
                    model.CompanyName = title;
                    model.SourceUrl = GetUrl();
                    model.Address = address;
                    model.Phone = phone;
                    model.CompanyUrl = companyURL;
                    modelData.Add(model);
                }
            }

            return modelData;
        }

        private void GetSubPageData(List<scrapperModel> modeldata)
        {
            foreach (var model in modeldata)
            {
                try
                {
                    if (model.DetailsPageUrl != string.Empty)
                    {
                        Console.WriteLine("Grabbing data for suburl" + model.DetailsPageUrl);
                        string outhtml = Helper.GetHtmlFromUrl(model.DetailsPageUrl);
                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(outhtml);
                        try
                        {
                            HtmlNode address = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'views-field-postal-code')]//span[contains(@class, 'field-content')]");
                            if (address != null)
                            {
                                model.Address = address.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch { }

                        try
                        {
                            HtmlNode phone = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'views-field-phone')]//span[contains(@class, 'field-content')]");
                            if (phone != null)
                            {
                                model.Phone = phone.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch { }

                        try
                        {

                            HtmlNode email = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'views-field-email')]//span[contains(@class, 'field-content')]");
                            if (email != null)
                            {
                                model.Email = email.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch
                        { }

                        try
                        {
                            HtmlNode website = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'views-field-url')]//span[contains(@class, 'field-content')]");
                            if (website != null)
                            {
                                model.CompanyUrl = website.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch { }

                        try
                        {
                            HtmlNode name = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'views-field-ceo-77')]//span[contains(@class, 'field-content')]");
                            if (name != null)
                            {
                                string contact = name.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                                KeyValuePair<string, string> contactdetail = Helper.GetFirstAndLastName(contact);
                                model.FirstName = contactdetail.Key;
                                model.LastName = contactdetail.Value;

                            }
                        }
                        catch { }
                        
                    }
                }
                catch { Helper.AddtoLogFile("Error in" + model.DetailsPageUrl); }
            }
        }
    }
}
