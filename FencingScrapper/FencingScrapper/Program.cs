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
        {
           
            Console.WriteLine("Welcome to Scrapper App");
            Console.WriteLine("Press 1 to extract Data from https://findaddictiontreatment.ny.gov");
            Console.WriteLine("Press 2 to extract Data from http://www.mkrecoverycoaching.com/recovery-coach-training-organizations/");
            Console.WriteLine("Press 3 to extract Data from https://www.naatp.org/resources/addiction-industry-directory");
            Console.WriteLine("Press 4 to extract Data from https://find.ohiorecoveryhousing.org");
            Console.WriteLine("Press 5 to extract Data from https://mashsoberhousing.org/certified-residences/");
            Console.WriteLine("Press 6 to extract Data from http://choopersguide.com/find-addiction-treatment/location/united-states-addiction-treatment");
            Console.WriteLine("Press 7 to extract Data from http://choopersguide.com/find-addiction-treatment/recovery-residences/search/empty/where/empty/mile/empty/zip/empty");
            Console.WriteLine("Press 8 to extract Data from http://www.inarr.org/listings/worthy-women-recovery-home/");
            Console.WriteLine("Press 9 to extract Data from http://www.halfwayhouses.org/members.html");
            Console.WriteLine("Press 10 to extract Data from https://local.thefix.com/sober-living/");
            Console.WriteLine("Press 11 to extract Data from http://farronline.org/certification/certified-residences/");

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
                case "5":
                    scrapper = new mashsoberhousingScrapper();
                    break;
                case "6":
                    scrapper = new choopersguidelocationScrapper();
                    break;
                case "7":
                    scrapper = new choopersguidelocationRecoveryScrapper();
                    break;
                case "8":
                    scrapper = new inarrScrapper();
                    break;
                case "9":
                    scrapper = new halfwayhousesScrrapper();
                    break;
                case "10":
                    scrapper = new thefixScrapper();
                    break;
                case "11":
                    scrapper = new farronlineScrapper();
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
