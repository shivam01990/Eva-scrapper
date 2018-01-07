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
           
        }

        public string GetUrl(int pageNo)
        {
            return "https://local.thefix.com/sober-living/page/"+pageNo;
        }

        public string GetUrl()
        {
            return "https://local.thefix.com/sober-living/";
        }
    }
}
