using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper
{
    public class GenrateReport
    {
        public static void StartGenerate(string fileparam,List<scrapperModel> scrapperData )
        {
            string BasedirectPath = AppDomain.CurrentDomain.BaseDirectory;
            string filename = "\\"+ fileparam + ".xlsx";
            string filepath = BasedirectPath + filename;
            var file = new FileInfo(filepath);
            var package = new ExcelPackage(file);

            int RowNum = 1;
            int ColNum = 1;
            using (ExcelWorksheet ws = package.Workbook.Worksheets.Add("Finance - " + DateTime.Now.ToShortDateString()))
            {
                ////Merging cells and create a center heading for out table
                //ws.Cells[RowNum, ColNum].Value = "Finance Companies"; // Heading Name
                //ws.Cells[RowNum, ColNum, RowNum, 9].Merge = true; //Merge columns start and end range               
                //ws.Cells[RowNum, ColNum, RowNum, 9].Style.Font.Size = 30; //Font should be bold                 
                //ws.Cells[RowNum, ColNum, RowNum, 9].Style.Font.Bold = true; //Font should be bold                  
                //ws.Cells[RowNum, ColNum, RowNum, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                //RowNum++;

                ws.Cells[RowNum, ColNum].Value = "Name of Company"; // Heading Name
                ws.Cells[RowNum, ColNum, RowNum, ColNum].Merge = true; //Merge columns start and end range
                ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.Fill.BackgroundColor.SetColor(Color.Black);
                ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.Font.Bold = true; //Font should be bold    
                ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.Font.Color.SetColor(Color.White);
                ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                ws.Cells[RowNum, ColNum + 1].Value = "URL"; // Heading Name
                ws.Cells[RowNum, ColNum + 1, RowNum, ColNum + 1].Merge = true; //Merge columns start and end range
                ws.Cells[RowNum, ColNum + 1, RowNum, ColNum + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[RowNum, ColNum + 1, RowNum, ColNum + 1].Style.Fill.BackgroundColor.SetColor(Color.Black);
                ws.Cells[RowNum, ColNum + 1, RowNum, ColNum + 1].Style.Font.Bold = true; //Font should be bold    
                ws.Cells[RowNum, ColNum + 1, RowNum, ColNum + 1].Style.Font.Color.SetColor(Color.White);
                ws.Cells[RowNum, ColNum + 1, RowNum, ColNum + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                ws.Cells[RowNum, ColNum + 2].Value = "phone"; // Heading Name
                ws.Cells[RowNum, ColNum + 2, RowNum, ColNum + 2].Merge = true; //Merge columns start and end range
                ws.Cells[RowNum, ColNum + 2, RowNum, ColNum + 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[RowNum, ColNum + 2, RowNum, ColNum + 2].Style.Fill.BackgroundColor.SetColor(Color.Black);
                ws.Cells[RowNum, ColNum + 2, RowNum, ColNum + 2].Style.Font.Bold = true; //Font should be bold    
                ws.Cells[RowNum, ColNum + 2, RowNum, ColNum + 2].Style.Font.Color.SetColor(Color.White);
                ws.Cells[RowNum, ColNum + 2, RowNum, ColNum + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                ws.Cells[RowNum, ColNum + 3].Value = "City"; // Heading Name
                ws.Cells[RowNum, ColNum + 3, RowNum, ColNum + 3].Merge = true; //Merge columns start and end range
                ws.Cells[RowNum, ColNum + 3, RowNum, ColNum + 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[RowNum, ColNum + 3, RowNum, ColNum + 3].Style.Fill.BackgroundColor.SetColor(Color.Black);
                ws.Cells[RowNum, ColNum + 3, RowNum, ColNum + 3].Style.Font.Bold = true; //Font should be bold    
                ws.Cells[RowNum, ColNum + 3, RowNum, ColNum + 3].Style.Font.Color.SetColor(Color.White);
                ws.Cells[RowNum, ColNum + 3, RowNum, ColNum + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                ws.Cells[RowNum, ColNum + 4].Value = "State"; // Heading Name
                ws.Cells[RowNum, ColNum + 4, RowNum, ColNum + 4].Merge = true; //Merge columns start and end range
                ws.Cells[RowNum, ColNum + 4, RowNum, ColNum + 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[RowNum, ColNum + 4, RowNum, ColNum + 4].Style.Fill.BackgroundColor.SetColor(Color.Black);
                ws.Cells[RowNum, ColNum + 4, RowNum, ColNum + 4].Style.Font.Bold = true; //Font should be bold    
                ws.Cells[RowNum, ColNum + 4, RowNum, ColNum + 4].Style.Font.Color.SetColor(Color.White);
                ws.Cells[RowNum, ColNum + 4, RowNum, ColNum + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                ws.Cells[RowNum, ColNum + 5].Value = "Company Url"; // Heading Name
                ws.Cells[RowNum, ColNum + 5, RowNum, ColNum + 5].Merge = true; //Merge columns start and end range
                ws.Cells[RowNum, ColNum + 5, RowNum, ColNum + 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[RowNum, ColNum + 5, RowNum, ColNum + 5].Style.Fill.BackgroundColor.SetColor(Color.Black);
                ws.Cells[RowNum, ColNum + 5, RowNum, ColNum + 5].Style.Font.Bold = true; //Font should be bold    
                ws.Cells[RowNum, ColNum + 5, RowNum, ColNum + 5].Style.Font.Color.SetColor(Color.White);
                ws.Cells[RowNum, ColNum + 5, RowNum, ColNum + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center
                

                ws.Cells[RowNum, ColNum + 6].Value = "Address"; // Heading Name
                ws.Cells[RowNum, ColNum + 6, RowNum, ColNum + 6].Merge = true; //Merge columns start and end range
                ws.Cells[RowNum, ColNum + 6, RowNum, ColNum + 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[RowNum, ColNum + 6, RowNum, ColNum + 6].Style.Fill.BackgroundColor.SetColor(Color.Black);
                ws.Cells[RowNum, ColNum + 6, RowNum, ColNum + 6].Style.Font.Bold = true; //Font should be bold    
                ws.Cells[RowNum, ColNum + 6, RowNum, ColNum + 6].Style.Font.Color.SetColor(Color.White);
                ws.Cells[RowNum, ColNum + 6, RowNum, ColNum + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                ws.Cells[RowNum, ColNum + 7].Value = "Email"; // Heading Name
                ws.Cells[RowNum, ColNum + 7, RowNum, ColNum + 7].Merge = true; //Merge columns start and end range
                ws.Cells[RowNum, ColNum + 7, RowNum, ColNum + 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[RowNum, ColNum + 7, RowNum, ColNum + 7].Style.Fill.BackgroundColor.SetColor(Color.Black);
                ws.Cells[RowNum, ColNum + 7, RowNum, ColNum + 7].Style.Font.Bold = true; //Font should be bold    
                ws.Cells[RowNum, ColNum + 7, RowNum, ColNum + 7].Style.Font.Color.SetColor(Color.White);
                ws.Cells[RowNum, ColNum + 7, RowNum, ColNum + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                ws.Cells[RowNum, ColNum + 8].Value = "First Name"; // Heading Name
                ws.Cells[RowNum, ColNum + 8, RowNum, ColNum + 8].Merge = true; //Merge columns start and end range
                ws.Cells[RowNum, ColNum + 8, RowNum, ColNum + 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[RowNum, ColNum + 8, RowNum, ColNum + 8].Style.Fill.BackgroundColor.SetColor(Color.Black);
                ws.Cells[RowNum, ColNum + 8, RowNum, ColNum + 8].Style.Font.Bold = true; //Font should be bold    
                ws.Cells[RowNum, ColNum + 8, RowNum, ColNum + 8].Style.Font.Color.SetColor(Color.White);
                ws.Cells[RowNum, ColNum + 8, RowNum, ColNum + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                ws.Cells[RowNum, ColNum + 9].Value = "Last Name"; // Heading Name
                ws.Cells[RowNum, ColNum + 9, RowNum, ColNum + 9].Merge = true; //Merge columns start and end range
                ws.Cells[RowNum, ColNum + 9, RowNum, ColNum + 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[RowNum, ColNum + 9, RowNum, ColNum + 9].Style.Fill.BackgroundColor.SetColor(Color.Black);
                ws.Cells[RowNum, ColNum + 9, RowNum, ColNum + 9].Style.Font.Bold = true; //Font should be bold    
                ws.Cells[RowNum, ColNum + 9, RowNum, ColNum + 9].Style.Font.Color.SetColor(Color.White);
                ws.Cells[RowNum, ColNum + 9, RowNum, ColNum + 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center
                RowNum++;

                int i = 1;
                foreach (var item in scrapperData)
                {
                    ws.Cells[RowNum, ColNum].Value = item.CompanyName;
                    ws.Cells[RowNum, ColNum + 1].Value = item.SourceUrl;
                    ws.Cells[RowNum, ColNum + 2].Value = item.Phone;
                    ws.Cells[RowNum, ColNum + 3].Value = item.City;
                    ws.Cells[RowNum, ColNum + 4].Value = item.State;
                    ws.Cells[RowNum, ColNum + 5].Value = item.CompanyUrl;
                    ws.Cells[RowNum, ColNum + 6].Value = item.Address;
                    ws.Cells[RowNum, ColNum + 7].Value = item.Email;
                    ws.Cells[RowNum, ColNum + 8].Value = item.FirstName;
                    ws.Cells[RowNum, ColNum + 9].Value = item.LastName;


                    i++;
                    RowNum++;
                }
                ws.Column(1).Width = 40;
                ws.Column(2).Width = 40;
                ws.Column(3).Width = 40;
                ws.Column(4).Width = 40;
                ws.Column(5).Width = 40;
                ws.Column(6).Width = 40;
                ws.Column(7).Width = 40;
                ws.Column(8).Width = 40;
                ws.Column(9).Width = 40;
                //save our new workbook and we are done!
                package.Save();
            }

        }
    }
}
