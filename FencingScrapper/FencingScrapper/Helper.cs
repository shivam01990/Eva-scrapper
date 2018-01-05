using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WatiN.Core;

namespace FencingScrapper
{
    public class Helper
    {
        #region--Generate Logs--
        public static void AddtoLogFile(string Message)
        {
            try
            {
                string LogPath = AppDomain.CurrentDomain.BaseDirectory;
                string filename = "\\Log.txt";
                string filepath = LogPath + filename;
                if (!File.Exists(filepath))
                {
                    StreamWriter writer = File.CreateText(filepath);
                    writer.Close();
                }
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    writer.WriteLine(Message);
                    Console.WriteLine(Message);
                }
            }
            catch
            { }
        }
        #endregion

        #region--Open IE URL--

        public static string OpenIEURL(string URL)
        {
            string _Html = "";
            IE ie = new IE();
            bool tryAgain = true;
            while (tryAgain)
            {

                try
                {
                    ie.GoTo(URL);
                    //Settings.WaitForCompleteTimeOut = 480;
                    System.Threading.Thread.Sleep(20000);
                    tryAgain = false;
                    _Html = ie.Html;
                    ie.Close();
                }
                catch (Exception ex)
                {
                    ie.Close();
                    ie = new IE();
                }
            }
            return _Html;
        }
        #endregion

        #region--Forward IE URL--
        public static IE ForwardIEURL(string URL, IE ie)
        {
            bool tryAgain = true;
            while (tryAgain)
            {

                try
                {
                    ie.Close();
                    ie = new IE();
                    ie.GoTo(URL);
                    // Settings.WaitForCompleteTimeOut = 480;
                    System.Threading.Thread.Sleep(5000);
                    tryAgain = false;
                }
                catch
                {

                }
            }
            return ie;
        }
        #endregion

        public static string GetHtmlFromUrl(string url)
        {
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                string htmlCode = client.DownloadString(url);
                return htmlCode;
            }
        }

        public static KeyValuePair<string, string> GetStateAndCity(string address)
        {
            int totalcount = address.Split(',').Length;
            string city = string.Empty;
            if (totalcount >= 2)
            {
                city = address.Split(',')[totalcount - 2];
                city = city.Split(' ').LastOrDefault();
            }
            string statezip = address.Trim().Split(',').LastOrDefault();
            string state = statezip.Trim().Split(' ').FirstOrDefault();
            return new KeyValuePair<string, string>(city, state);
        }
    }
}
