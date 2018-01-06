using FencingScrapper.Scrapper;
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
            Console.WriteLine("Press 2 to extract Data from http://www.mkrecoverycoaching.com/recovery-coach-training-organizations/");
            Console.WriteLine("Press 3 to extract Data from https://www.naatp.org/resources/addiction-industry-directory");
            Console.WriteLine("Press 4 to extract Data from https://find.ohiorecoveryhousing.org");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    scrapper = new findaddictiontreatmentScrapper();
                    break;
                case "2":
                    scrapper = new mkrecoverycoachingScrapper();
                    break;
                case "3":
                    scrapper = new naatpScrapper();
                    break;
                case "4":
                    scrapper = new ohiorecoveryhousingScrapper();
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
