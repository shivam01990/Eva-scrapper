using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    public class yelpScrapper : baseScrapper, IScrapper
    {


        private List<string> GetStates()
        {
            return new List<string>()
            {
                "AK",
                //"AL",
                //"AR",
                //"AZ",
                //"CA",
                //"CO",
                //"CT",
                //"DC",
                //"DE",
                //"FL",
                //"GA",
                //"HI",
                //"IA",
                //"ID",
                //"IL",
                //"IN",
                //"KS",
                //"KY",
                //"LA",
                //"MA",
                //"MD",
                //"ME",
                //"MI",
                //"MN",
                //"MO",
                //"MS",
                //"MT",
                //"NC",
                //"ND",
                //"NE",
                //"NH",
                //"NJ",
                //"NM",
                //"NV",
                //"NY",
                //"OH",
                //"OK",
                //"OR",
                //"PA",
                //"RI",
                //"SC",
                //"SD",
                //"TN",
                //"TX",
                //"UT",
                //"VA",
                //"VT",
                //"WA",
                //"WI",
                //"WV",
                //"WY"
            };
        }

        public List<string> GetServices()
        {
            return new List<string>() { "recovery+services", "sober+living" };
        }

        private string GetUrl(int pageNo, string states, string service)
        {
            int startPointer = pageNo * 10;
            if (pageNo > 0)
            {
                startPointer = startPointer - 10;
            }
            return GetUrl(states, service) + "&start=" + startPointer;
        }

        private string GetUrl(string states, string service)
        {
            return GetUrl() + "search?find_desc=" + service + "&find_loc=" + states;
        }

        private void GetDataForState(string service, string state)
        {
            int totalPages = 0;
            int currentPage = 0;
            do
            {

                Console.WriteLine("Grabbing data for " + GetUrl(currentPage, state, service));
                HtmlDocument doc = Helper.GetHtmlDocFromUrl(GetUrl(currentPage, state, service));
                HtmlNode toalPagesNode = doc.DocumentNode.SelectSingleNode("//div[@class='page-of-pages arrange_unit arrange_unit--fill']");

                // HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//table[contains(@class, 'table-striped')]//tr");

                currentPage = currentPage + 1;
            } while (currentPage <= totalPages);

        }

        public void ExtractData()
        {
            foreach (string service in GetServices())
            {
                foreach (var state in GetStates())
                {
                    GetDataForState(service, state);
                }
            }
        }

        public string GetUrl()
        {
            return "https://www.yelp.com/";
        }
    }
}
