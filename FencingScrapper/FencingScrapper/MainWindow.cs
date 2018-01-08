using FencingScrapper.Scrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FencingScrapper
{
    public partial class MainWindow : Form
    {
        private baseScrapper scrapperInstance;

        public MainWindow(string url, baseScrapper scrapperobj)
        {
            InitializeComponent();
            scrapperInstance = scrapperobj;
            Uri webaddress = new Uri(url);
            webBrowserControl.ScriptErrorsSuppressed = true;
            webBrowserControl.Url = webaddress;

        }

        private void btnScrapData_Click(object sender, EventArgs e)
        {
            scrapperInstance.ScraperEventHandler(webBrowserControl.Document.Body.OuterHtml, null);
            MessageBox.Show("Scrapping complete please navigate for next page", "Alert Popup.");
        }
    }
}
