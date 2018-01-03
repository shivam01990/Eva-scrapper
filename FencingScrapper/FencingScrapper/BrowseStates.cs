using HtmlAgilityPack;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WatiN.Core;

namespace FencingScrapper
{
    public class BrowseStates
    {
        public static void GrabData()
        {
            try
            {
                Helper.AddtoLogFile("fetching data from" + Helper.GetSiteURL());

                string outhtml = Helper.OpenIEURL(Helper.GetSiteURL());
                // Write out the HTML text of the body
                Console.WriteLine(outhtml);
                
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(outhtml);
                HtmlNodeCollection anchorStates = doc.DocumentNode.SelectNodes("(//div[contains(@class, 'well') and contains(@class, 'well-sm')]//div[@class='row phn'])[2]//ul//li//a");

                List<StateList> _statelist = new List<StateList>();
                if (anchorStates != null)
                {
                    foreach (var link in anchorStates)
                    {
                        StateList temp = new StateList();
                        temp.StateUrl = link.Attributes["href"].Value;
                        temp.StateName = link.InnerText;
                        _statelist.Add(temp);
                    }
                }

                string BasedirectPath = AppDomain.CurrentDomain.BaseDirectory;
                string filename = "\\Finance.xlsx";
                string filepath = BasedirectPath + filename;
                var file = new FileInfo(filepath);
                using (var package = new ExcelPackage(file))
                {
                    int RowNum = 1;
                    int ColNum = 1;
                    ExcelWorksheet ws = package.Workbook.Worksheets.Add("Finance - " + DateTime.Now.ToShortDateString());
                    //Merging cells and create a center heading for out table
                    ws.Cells[RowNum, ColNum].Value = "Finance Companies"; // Heading Name
                    ws.Cells[RowNum, ColNum, RowNum, 9].Merge = true; //Merge columns start and end range               
                    ws.Cells[RowNum, ColNum, RowNum, 9].Style.Font.Size = 30; //Font should be bold                 
                    ws.Cells[RowNum, ColNum, RowNum, 9].Style.Font.Bold = true; //Font should be bold                  
                    ws.Cells[RowNum, ColNum, RowNum, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                    RowNum++;


                    ws.Cells[RowNum, ColNum].Value = "Comany Name"; // Heading Name
                    ws.Cells[RowNum, ColNum, RowNum, ColNum].Merge = true; //Merge columns start and end range
                    ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.Fill.BackgroundColor.SetColor(Color.Black);
                    ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.Font.Bold = true; //Font should be bold    
                    ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.Font.Color.SetColor(Color.White);
                    ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                    ws.Cells[RowNum, ColNum + 1].Value = "Email"; // Heading Name
                    ws.Cells[RowNum, ColNum + 1, RowNum, ColNum + 1].Merge = true; //Merge columns start and end range
                    ws.Cells[RowNum, ColNum + 1, RowNum, ColNum + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[RowNum, ColNum + 1, RowNum, ColNum + 1].Style.Fill.BackgroundColor.SetColor(Color.Black);
                    ws.Cells[RowNum, ColNum + 1, RowNum, ColNum + 1].Style.Font.Bold = true; //Font should be bold    
                    ws.Cells[RowNum, ColNum + 1, RowNum, ColNum + 1].Style.Font.Color.SetColor(Color.White);
                    ws.Cells[RowNum, ColNum + 1, RowNum, ColNum + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center
                    RowNum++;

                    _statelist.Remove(_statelist.Where(S => S.StateName == "Alabama").FirstOrDefault());
                    _statelist.Remove(_statelist.Where(S => S.StateName == "Alaska").FirstOrDefault());
                    _statelist.Remove(_statelist.Where(S => S.StateName == "Arizona").FirstOrDefault());
                    _statelist.Remove(_statelist.Where(S => S.StateName == "Arkansas").FirstOrDefault());
                    _statelist.Remove(_statelist.Where(S => S.StateName == "California").FirstOrDefault());
                    _statelist.Remove(_statelist.Where(S => S.StateName == "Colorado").FirstOrDefault());
                    _statelist.Remove(_statelist.Where(S => S.StateName == "Connecticut").FirstOrDefault());
                    _statelist.Remove(_statelist.Where(S => S.StateName == "Delaware").FirstOrDefault());
                    _statelist.Remove(_statelist.Where(S => S.StateName == "District of Columbia").FirstOrDefault());
                    _statelist.Remove(_statelist.Where(S => S.StateName == "Florida").FirstOrDefault());
                 
                    foreach (StateList state in _statelist)
                    {
                        string tempurl = "http://www.manta.com" + state.StateUrl;
                        state.StateUrl = tempurl;
                        StateProvider.StartScrapFirstPage(state);
                        RowNum++;
                        //break;
                    }
                    ws.Column(1).Width = 40;
                    ws.Column(2).Width = 15;
                    // save our new workbook and we are done!
                    package.Save();
                }


                // Close Internet Explorer and the console window immediately.
                //ie.Close();

            }
            catch (Exception ex)
            {
                Helper.AddtoLogFile("Error:" + ex.ToString());
            }

        }
    }
}
