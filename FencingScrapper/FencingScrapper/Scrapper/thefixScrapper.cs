using FencingScrapper.DB;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    public class thefixScrapper : IScrapper
    {
        public void ExtractData()
        {
            List<scrapperModel> modelData = new List<scrapperModel>();

            int totalPages = 1590;
           

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
            GenrateReport.StartGenerate("thefix", modelData);

        }

        private List<scrapperModel> GetData(int pageNo)
        {
            HtmlDocument doc = Helper.GetHtmlDocFromUrl(GetUrl(pageNo));
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//table[contains(@class, 'table-striped')]//tr");
            List<scrapperModel> modelData = new List<scrapperModel>();
            for (int i = 0; i < items.Count; i++)
            {
                scrapperModel model = new scrapperModel();
                model.SourceUrl = GetUrl();
                model.PagingURL = GetUrl(pageNo);

                HtmlNodeCollection columns = items[i].SelectNodes(".//td");
                if (columns != null & columns.Count == 2)
                {
                    try
                    {
                        HtmlNode titlenode = columns[1].SelectSingleNode(".//h5");
                        if (titlenode != null)
                        {
                            model.CompanyName = titlenode.InnerText.Replace("\n", " ").Replace("\r", " ");
                            HtmlNode anchortag = titlenode.SelectSingleNode(".//a");

                            if (anchortag != null)
                            {
                                try
                                {
                                    model.DetailsPageUrl = anchortag.GetAttributeValue("href", string.Empty).Replace("\n", " ").Replace("\r", " ").Replace("&amp;", " ").Replace("&nbsp;", " ");
                                }
                                catch { }
                            }
                        }

                        HtmlNode addressnode = columns[1].SelectSingleNode(".//p[contains(@class, 'address')]");
                        if (addressnode != null)
                        {

                            model.Address = addressnode.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            KeyValuePair<string, string> address = Helper.GetStateAndCity(model.Address);
                            model.State = address.Value;
                            model.City = address.Key;
                        }
                    }
                    catch { }
                    model.SourceUrl = GetUrl();
                    model = ScrapperProvider.SaveScrapper(model);
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
                            HtmlNode phone = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'phone')]");
                            if (phone != null)
                            {
                                model.Phone = phone.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch { }

                        try
                        {

                            HtmlNode email = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'email')]");
                            if (email != null)
                            {
                                model.Email = email.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch
                        { }

                        try
                        {
                            HtmlNode website = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'website')]");
                            if (website != null)
                            {
                                model.CompanyUrl = website.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch { }

                        model.IsDetailsPageScrapped = true;
                        ScrapperProvider.SaveScrapper(model);

                    }
                }
                catch { Helper.AddtoLogFile("Error in" + model.DetailsPageUrl); }
            }
        }

        public string GetUrl(int pageNo)
        {
            return "https://local.thefix.com/sober-living/page/" + pageNo;
        }

        public string GetUrl()
        {
            return "https://local.thefix.com/sober-living/";
        }
    }
}
