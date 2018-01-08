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
using CefSharp;
using CefSharp.WinForms;


namespace FencingScrapper
{
    public partial class MainWindow : Form
    {
        private baseScrapper scrapperInstance;
        public ChromiumWebBrowser chromeBrowser;
        public MainWindow(string url, baseScrapper scrapperobj)
        {
            InitializeComponent();
            scrapperInstance = scrapperobj;
            //Uri webaddress = new Uri(url);
            InitializeChromium(url);
            //webBrowserControl.ScriptErrorsSuppressed = true;
            //webBrowserControl.Url = webaddress;

        }


        public void InitializeChromium(string url)
        {
            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            Cef.Initialize(settings);
            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser(url);
            // Add it to the form and fill it to the form window.
            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
        }

        private void btnScrapData_Click(object sender, EventArgs e)
        {
            string source =  chromeBrowser.GetBrowser().MainFrame.GetSourceAsync().Result;
            //scrapperInstance.ScraperEventHandler(webBrowserControl.Document.Body.OuterHtml, null);
            MessageBox.Show("Scrapping complete please navigate for next page", "Alert Popup.");
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
