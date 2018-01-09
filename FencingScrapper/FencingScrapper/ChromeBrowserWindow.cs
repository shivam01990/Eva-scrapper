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
using FencingScrapper.Scrapper;

namespace FencingScrapper
{
    public partial class ChromeBrowserWindow : Form
    {
        private baseScrapper scrapperInstance;
        public ChromiumWebBrowser chromeBrowser;

        public ChromeBrowserWindow(string url, baseScrapper scrapperobj)
        {
            InitializeComponent();
            scrapperInstance = scrapperobj;            
            InitializeChromium(url);
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
            string source = chromeBrowser.GetBrowser().MainFrame.GetSourceAsync().Result;
            scrapperInstance.ScraperEventHandler(source, null);
            MessageBox.Show("Scrapping complete please navigate for next page", "Alert Popup.");
        }

       

        private void ChromeBrowserWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
