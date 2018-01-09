using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace FencingScrapper.Scrapper
{
    public class myazrhaScrapper : baseScrapper, IScrapper
    {
        public void ExtractData()
        {
            List<scrapperModel> modelData = new List<scrapperModel>();

            int totalPages = 7;
            //int totalPages = 0;

            for (int i = 0; i <= totalPages; i++)
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
            GenrateReport.StartGenerate("myazrha", modelData);
        }


        private List<scrapperModel> GetData(int pageNo)
        {
            HtmlDocument doc = Helper.GetHtmlDocFromUrl(GetUrl(pageNo));
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//div[contains(@class, 'view-azrha-locations')]//h3");
            List<scrapperModel> modelData = new List<scrapperModel>();
            for (int i = 0; i < items.Count; i++)
            {
                scrapperModel model = new scrapperModel();


                try
                {
                    model.CompanyName = items[i].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("&#039;", "'");
                    HtmlNode anchortag = items[i].SelectSingleNode(".//a");

                    if (anchortag != null)
                    {
                        try
                        {
                            model.DetailsPageUrl = anchortag.GetAttributeValue("href", string.Empty).Replace("\n", " ").Replace("\r", " ").Replace("&amp;", " ").Replace("&nbsp;", " ");
                            if (model.DetailsPageUrl != string.Empty)
                            {
                                model.DetailsPageUrl = "http://www.myazrha.org" + model.DetailsPageUrl;
                            }
                        }
                        catch { }
                    }

                }
                catch { }
                model.SourceUrl = GetUrl();
                modelData.Add(model);

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
                            HtmlNode companyname = doc.DocumentNode.SelectSingleNode("//h5[contains(@class, 'views-field-field-company-name')]//span");
                            if (companyname != null)
                            {
                                string company = companyname.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ").Replace("Address", "");
                                if (company.Length > 0)
                                {
                                    model.CompanyName = model.CompanyName + "(" + company + ")";
                                }
                            }
                        }
                        catch { }

                        try
                        {
                            HtmlNode address = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'views-field-field-company-address')]//span");
                            if (address != null)
                            {
                                model.Address = address.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ").Replace("Address", "");
                                KeyValuePair<string, string> citynstate = Helper.GetStateAndCity(model.Address);                              

                            }
                        }
                        catch { }

                        try
                        {
                            HtmlNode namenode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'views-field-field-company-contact')]//div");
                            if (namenode != null)
                            {
                                string name = namenode.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ").Replace("Address", "");
                                KeyValuePair<string, string> namepair = Helper.GetFirstAndLastName(name);
                                model.FirstName = namepair.Key;
                                model.LastName = namepair.Value;
                            }
                        }
                        catch { }



                        try
                        {

                            HtmlNode websitenodes = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'views-field-field-company-website')]//a");
                            if (websitenodes != null)
                            {
                                model.CompanyUrl = websitenodes.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch
                        { }



                        try
                        {

                            HtmlNode emailnodes = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'views-field-field-contact-email')]//a");
                            if (emailnodes != null)
                            {
                                model.Email = emailnodes.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch
                        { }

                        try
                        {

                            HtmlNode citynodes = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'views-field-field-company-city')]//span");
                            if (citynodes != null)
                            {
                                model.City = citynodes.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch
                        { }

                        try
                        {

                            HtmlNode phonenode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'views-field-field-contact-telephone')]//div");
                            if (phonenode != null)
                            {
                                model.Phone = phonenode.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("&amp;", " ");
                            }
                        }
                        catch
                        { }


                    }
                }
                catch { Helper.AddtoLogFile("Error in" + model.DetailsPageUrl); }
            }
        }


        public string GetUrl()
        {
            return "http://www.myazrha.org/azrha-locations";
        }

        public string GetUrl(int pageNo)
        {
            return "http://www.myazrha.org/azrha-locations?page=" + pageNo;
        }
    }
}
