using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    public class inarrScrapper : IScrapper
    {
        public void ExtractData()
        {

            List<scrapperModel> modelData = new List<scrapperModel>();

            int totalPages = 6;

            for (int i = 1; i <= totalPages; i++)
            {
                Console.WriteLine("Grabbing data for " + GetUrl(i));
                try
                {

                    List<scrapperModel> data = GetData(i);
                    modelData.AddRange(data);
                }
                catch (Exception ex)
                {
                    Helper.AddtoLogFile("Error in" + GetUrl(i));
                }
            }

            GetSubPageData(modelData);
            GenrateReport.StartGenerate("inarr", modelData);
        }


        private List<scrapperModel> GetData(int pageNo)
        {
            string outhtml = Helper.GetHtmlFromUrl(GetUrl(pageNo));
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//div[contains(@id, 'listings-list-row')]");
            List<scrapperModel> modelData = new List<scrapperModel>();
            for (int i = 0; i < items.Count; i++)
            {
                scrapperModel model = new scrapperModel();

                HtmlNode titlenode = items[i].SelectSingleNode(".//div[contains(@class, 'acadp-listings-title-block')]");
                if (titlenode != null)
                {
                    try
                    {
                        model.CompanyName = titlenode.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Replace("  ", "");
                        HtmlNode anchortag = titlenode.SelectSingleNode(".//a");

                        if (anchortag != null)
                        {
                            try
                            {
                                model.DetailsPageUrl = anchortag.GetAttributeValue("href", string.Empty).Replace("\n", " ").Replace("\r", " ").Replace("&amp;", " ").Replace("&nbsp;", " ");
                                if (model.DetailsPageUrl != string.Empty)
                                {
                                    model.DetailsPageUrl = model.DetailsPageUrl;
                                }
                            }
                            catch { }
                        }

                        HtmlNodeCollection Contacts = items[i].SelectNodes(".//div[contains(@class, 'div-address')]//p//br");
                        if (Contacts != null)
                        {
                            string add1 = "";
                            string add2 = "";
                            if (Contacts.Count >= 1)
                            {
                                add1 = Contacts[0].PreviousSibling.InnerText.Replace("\n", " ").Replace("\r", " ");
                            }

                            if (Contacts.Count >= 2)
                            {
                                add2 = Contacts[1].PreviousSibling.InnerText.Replace("\n", " ").Replace("\r", " ");
                            }

                            model.Address = add1 + add2;

                            KeyValuePair<string, string> cityandState = Helper.GetStateAndCity(model.Address);
                            model.City = cityandState.Key;
                            model.State = cityandState.Value;
                            if (Contacts.Count >= 3)
                            {
                                model.Phone = Contacts[2].PreviousSibling.InnerText.Replace("\n", " ").Replace("\r", " "); ;
                            }

                        }

                    }
                    catch { }
                    model.Url = GetUrl();
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
                            HtmlNode website = doc.DocumentNode.SelectSingleNode("//a[contains(@class, 'btn-success')]");
                            if (website != null)
                            {
                                model.CompanyUrl = website.GetAttributeValue("href", string.Empty).Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch { }
                    }
                }
                catch { Helper.AddtoLogFile("Error in" + model.DetailsPageUrl); }
            }
        }

        public string GetUrl()
        {
            return "http://www.inarr.org/recovery-residences/";
        }

        public string GetUrl(int pageNo)
        {
            return "http://www.inarr.org/search-listings/page/" + pageNo + "/?q&location=20&category=-1";
        }
    }
}
