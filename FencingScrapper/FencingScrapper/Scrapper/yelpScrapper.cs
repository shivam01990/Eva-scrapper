using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FencingScrapper.DB;

namespace FencingScrapper.Scrapper
{
    public class yelpScrapper : baseScrapper, IScrapper
    {


        private List<string> GetStates()
        {
            return new List<string>()
            {
                //"AK", // Done
                //"AL", // Done
                //"AR", //Done
                //"AZ", //Done
                //"CA", //Done
                //"CO", //Done
                //"CT", //Done
                //"DC", //Done
                //"DE", //Done
                //"FL", //Done
                //"GA", //Done
                //"HI", //Done
                //"IA", //Done
                //"ID", //Done
                //"IL", //DOne
                //"IN", //Done
                //"KS", //Done
                //"KY", //Done
                //"LA",   //Done
                //"MA",   //Done
                //"MD",   //Done
                //"ME", //Done
                //"MI", //Done
                //"MN", //Done
                //"MO", //Done
                //"MS", //Done
                //"MT", //Done
                //"NC", //Done
                //"ND", //Done
                //"NE", //Done
                //"NH", //Done
                //"NJ", //Done
                //"NM", //Done
                //"NV", //Done
                //"NY", //Done
                //"OH", //Done
                //"OK", //Done
                "OR",
                "PA",
                "RI",
                "SC",
                "SD",
                "TN",
                "TX",
                "UT",
                "VA",
                "VT",
                "WA",
                "WI",
                "WV",
                "WY"
            };
        }

        public List<string> GetServices()
        {
            return new List<string>() { "recovery+services", "sober+living" };
        }

        private string GetUrl(int pageNo, string states, string service)
        {
            int startPointer = pageNo * 10;
            startPointer = startPointer - 10;
            return GetUrl(states, service) + "&start=" + startPointer;
        }

        private string GetUrl(string states, string service)
        {
            return GetUrl() + "search?find_desc=" + service + "&find_loc=" + states;
        }

        private List<scrapperModel> GetDataForState(string service, string state)
        {
            int totalPages = 1;
            int currentPage = 1;
            List<scrapperModel> modelData = new List<scrapperModel>();

            while (currentPage <= totalPages)
            {

                Console.WriteLine("Grabbing data for " + GetUrl(currentPage, state, service));
                HtmlDocument doc = Helper.GetHtmlDocFromUrl(GetUrl(currentPage, state, service));
                HtmlNode toalPagesNode = doc.DocumentNode.SelectSingleNode("//div[@class='page-of-pages arrange_unit arrange_unit--fill']");
                if (toalPagesNode != null)
                {
                    string pagingtext = toalPagesNode.InnerText.Replace("\n", "").Replace("\r", "");
                    string totalpagestr = pagingtext.Split('f').LastOrDefault().Trim();
                    int.TryParse(totalpagestr, out totalPages);
                }

                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//ul//li[@class='regular-search-result']");
                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {
                        scrapperModel model = new scrapperModel();
                        model.SourceUrl = GetUrl();
                        HtmlNode titlenode = node.SelectSingleNode(".//h3[@class='search-result-title']//a");
                        if (titlenode != null)
                        {
                            model.CompanyName = titlenode.InnerText.Replace("\n", " ").Replace("\r", " ");

                            try
                            {
                                model.DetailsPageUrl = "https://www.yelp.com" + titlenode.GetAttributeValue("href", string.Empty).Replace("\n", " ").Replace("\r", " ").Replace("&amp;", " ").Replace("&nbsp;", " ");
                            }
                            catch { }

                        }

                        HtmlNode phonenode = node.SelectSingleNode(".//div[@class='secondary-attributes']//span[@class='biz-phone']");
                        if (phonenode != null)
                        {
                            model.Phone = phonenode.InnerText.Replace("\n", "").Replace("\r", "").Trim();
                        }


                        HtmlNode address = node.SelectSingleNode(".//div[@class='secondary-attributes']//address");
                        if (address != null)
                        {
                            model.Address = address.InnerText.Replace("\n", "").Replace("\r", "").Trim();
                            KeyValuePair<string, string> citynstate = Helper.GetStateAndCity(model.Address);
                            model.City = citynstate.Key;
                            model.State = citynstate.Value;                           
                        }
                        model.State = state;
                        HtmlNode price = node.SelectSingleNode(".//div[@class='main-attributes']//div[@class='media-story']//span[@class='business-attribute price-range']");
                        if (price != null)
                        {
                            model.Prices = price.InnerText.Replace("\n", "").Replace("\r", "").Trim();
                        }

                        model = ScrapperProvider.SaveScrapper(model);
                        modelData.Add(model);

                    }

                }
                // HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//table[contains(@class, 'table-striped')]//tr");

                currentPage = currentPage + 1;
            }

            return modelData;
        }

        public void ExtractData()
        {
            foreach (string state in GetStates())
            {
                foreach (var service in GetServices())
                {
                    List<scrapperModel> modelData = GetDataForState(service, state);
                    GenrateReport.StartGenerate("yelp_" + service.Replace("+", "") + "_" + state, modelData);
                }
            }
        }

        public string GetUrl()
        {
            return "https://www.yelp.com/";
        }
    }
}
