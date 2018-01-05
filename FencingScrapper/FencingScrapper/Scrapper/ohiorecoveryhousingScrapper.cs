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
            //Logic need to be implement
        }

        public string GetUrl()
        {
            return "https://find.ohiorecoveryhousing.org/?orhl_sc_gender&orhl_sc_within=9999&orhl_sc_location&orhl_sc_NARR_level&orhl_search=Search%20Now#orhl_search_results";
        }
    }
}
