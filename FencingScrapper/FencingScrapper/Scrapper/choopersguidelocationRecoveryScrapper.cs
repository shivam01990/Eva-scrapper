using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    public class choopersguidelocationRecoveryScrapper : IScrapper
    {
        private string GetUrl(int pageNo)
        {
            return GetUrl() + "/page/" + pageNo;
        }

        public string GetUrl()
        {
            return "http://choopersguide.com/find-addiction-treatment/recovery_residences/search/empty/where/empty/mile/empty/zip/empty";
        }

        public void ExtractData()
        {
            List<scrapperModel> modelData = new List<scrapperModel>();

            int totalPages = 74;
            //int totalPages = 1;

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
            GenrateReport.StartGenerate("choopersguidelocationRecovery", modelData);
        }

        private List<scrapperModel> GetData(int pageNo)
        {
            //string outhtml = Helper.GetHtmlFromUrl(GetUrl(pageNo));
            HtmlDocument doc = Helper.GetHtmlDocFromUrl(GetUrl(pageNo));
            //doc.LoadHtml(outhtml);
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//table[contains(@class, 'table_summary')]//tr");
            List<scrapperModel> modelData = new List<scrapperModel>();
            for (int i = 0; i < items.Count; i++)
            {
                scrapperModel model = new scrapperModel();

                HtmlNodeCollection columns = items[i].SelectNodes(".//td");
                if (columns != null)
                {
                    try
                    {
                        model.CompanyName = columns[1].InnerText.Replace("\n", " ").Replace("\r", " ");
                        HtmlNode anchortag = columns[1].SelectSingleNode(".//a");

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

                        model.City = columns[3].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                        model.State = columns[4].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                        model.Phone = columns[6].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
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
                            HtmlNode address = doc.DocumentNode.SelectSingleNode("//div[contains(@id, 'detail_right')]//address");
                            if (address != null)
                            {
                                model.Address = address.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ").Replace("Address","").Replace("                                                                           ","").Replace("                        ", "");
                            }
                        }
                        catch { }



                        try
                        {

                            HtmlNodeCollection websitenodes = doc.DocumentNode.SelectNodes("//div[contains(@id, 'detail_right')]//a");
                            if (websitenodes != null)
                            {
                                foreach (var website in websitenodes)
                                {
                                    string websiteurl = website.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                                    if (websiteurl.Contains("www."))
                                    {
                                        model.CompanyUrl = websiteurl;
                                    }
                                }
                            }
                        }
                        catch
                        { }




                    }
                }
                catch { Helper.AddtoLogFile("Error in" + model.DetailsPageUrl); }
            }
        }


    }
}
