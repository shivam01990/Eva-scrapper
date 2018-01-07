using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    public class farronlineScrapper : baseScrapper, IScrapper
    {
        public farronlineScrapper()
        {
            this.ScraperEventHandler += OnEventExecute;
        }

        private void OnEventExecute(object sender, EventArgs e)
        {
            string outhtml = (string)sender;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
        }

        public void ExtractData()
        {
            MainWindow browser = new MainWindow(GetUrl(), this);
            browser.ShowDialog();
        }

        public string GetUrl()
        {
            return "http://farronline.org/certification/certified-residences";
        }
    }
}
