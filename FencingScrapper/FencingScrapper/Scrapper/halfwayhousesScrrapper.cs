using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.Scrapper
{
    class halfwayhousesScrrapper : IScrapper
    {

        public string GetUrl()
        {
            return "http://www.halfwayhouses.org/members.html";
        }

        public void ExtractData()
        {
            List<scrapperModel> modelData = new List<scrapperModel>();
            string outhtml = Helper.GetHtmlFromUrl(GetUrl());
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(outhtml);
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//table//table//table//tr");
            for (int i = 1; i < items.Count; i++)
            {
                HtmlNodeCollection tdnode = items[i].SelectNodes(".//td");
                if (tdnode.Count == 2)
                {
                    scrapperModel model = new scrapperModel();
                    model.CompanyName = tdnode[0].InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("  ", " ");
                    HtmlNodeCollection Contacts = tdnode[1].SelectNodes(".//br");
                    if (Contacts != null)
                    {
                        if (Contacts.Count >= 2 && Contacts[1].PreviousSibling!=null)
                        {
                            model.Address = Contacts[1].PreviousSibling.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("  ", " ");
                        }

                        if (Contacts[Contacts.Count-1].NextSibling != null)
                        {
                            string output = Contacts[Contacts.Count - 1].NextSibling.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("  ", " ").Replace("&nbsp;", "");

                            if (!output.Contains("HTML"))
                            {
                                if (!(output.Contains("Telephone:") && output.Contains("Contact:")))
                                {
                                    if (output.Contains("Telephone:"))
                                    {
                                        model.Phone = output.Replace("Telephone:", "");
                                    }
                                    if (output.Contains("Contact:"))
                                    {
                                        KeyValuePair<string, string> contactdetail = Helper.GetFirstAndLastName(output.Replace("Contact:", ""));
                                        model.FirstName = contactdetail.Key;
                                        model.LastName = contactdetail.Value;

                                    }
                                    if (output.Contains("E-Mail:"))
                                    {
                                        model.Email = output.Replace("E-Mail:", "");

                                    }
                                    if (output.Contains("Email:"))
                                    {
                                        model.Email = output.Replace("Email:", "");

                                    }
                                    if (output.Contains("Web:"))
                                    {
                                        model.CompanyUrl = output.Replace("Web:", "");
                                    }
                                    model.Url = GetUrl();

                                }
                            }
                        }

                            foreach (var contact in Contacts)
                        {

                            if (contact.PreviousSibling != null)
                            {
                                
                                string output = contact.PreviousSibling.InnerText.Replace("\n", " ").Replace("\r", " ").Replace("\t", "").Replace("  ", " ").Replace("&nbsp;", "");

                                if (!output.Contains("HTML"))
                                {
                                    if (!(output.Contains("Telephone:") && output.Contains("Contact:")))
                                    {
                                        if (output.Contains("Telephone:"))
                                        {
                                            model.Phone = output.Replace("Telephone:", "");
                                        }
                                        if (output.Contains("Contact:"))
                                        {
                                            KeyValuePair<string, string> contactdetail = Helper.GetFirstAndLastName(output.Replace("Contact:", ""));
                                            model.FirstName = contactdetail.Key;
                                            model.LastName = contactdetail.Value;

                                        }
                                        if (output.Contains("E-Mail:"))
                                        {
                                            model.Email = output.Replace("E-Mail:", "");

                                        }
                                        if (output.Contains("Email:"))
                                        {
                                            model.Email = output.Replace("Email:", "");

                                        }
                                        if (output.Contains("Web:"))
                                        {
                                            model.CompanyUrl = output.Replace("Web:", "");
                                        }
                                        model.Url = GetUrl();
                                        
                                    }
                                }
                            }
                        }

                    }
                    modelData.Add(model);
                }
                
            }
            GenrateReport.StartGenerate("halfwayhouses", modelData);
        }
    }
}