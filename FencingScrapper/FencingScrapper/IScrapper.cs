using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper
{
    internal interface IScrapper
    {
        string GetUrl();
        void ExtractData();
    }
}
