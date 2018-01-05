using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    public class naatpScrapper : IScrapper
    {
        public void ExtractData()
        {
            //Logic need to be implement
        }

        private string GetUrl(int pageNo)
        {
            return GetUrl() + "?page="+pageNo;
        }

        public string GetUrl()
        {
            return "https://www.naatp.org/resources/addiction-industry-directory";
        }
    }
}
