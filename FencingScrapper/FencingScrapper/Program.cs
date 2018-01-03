using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper
{
    class Program
    {
        public static IScrapper scrapper;

        [STAThread]
        static void Main(string[] args)
        {            //public IScrapper scrapper = null;
            Console.WriteLine("Welcome to Scrapper App");
            Console.WriteLine("Press 1 to extract Data from https://findaddictiontreatment.ny.gov");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    scrapper = new findaddictiontreatmentScrapper();
                    break;
                default:
                    break;
            }

            if (scrapper != null)
            {
                scrapper.ExtractData();
            }
        }
    }
}
