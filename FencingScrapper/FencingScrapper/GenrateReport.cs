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
       public static void StartGenerate()
       {
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

                    ws.Cells[RowNum, ColNum].Value = "Sr.No"; // Heading Name
                    ws.Cells[RowNum, ColNum, RowNum, ColNum].Merge = true; //Merge columns start and end range
                    ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.Fill.BackgroundColor.SetColor(Color.Black);
                    ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.Font.Bold = true; //Font should be bold    
                    ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.Font.Color.SetColor(Color.White);
                    ws.Cells[RowNum, ColNum, RowNum, ColNum].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                    ws.Cells[RowNum, ColNum+1].Value = "Company Name"; // Heading Name
                    ws.Cells[RowNum, ColNum+1, RowNum, ColNum+1].Merge = true; //Merge columns start and end range
                    ws.Cells[RowNum, ColNum+1, RowNum, ColNum+1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[RowNum, ColNum+1, RowNum, ColNum+1].Style.Fill.BackgroundColor.SetColor(Color.Black);
                    ws.Cells[RowNum, ColNum+1, RowNum, ColNum+1].Style.Font.Bold = true; //Font should be bold    
                    ws.Cells[RowNum, ColNum+1, RowNum, ColNum+1].Style.Font.Color.SetColor(Color.White);
                    ws.Cells[RowNum, ColNum+1, RowNum, ColNum+1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                    ws.Cells[RowNum, ColNum + 2].Value = "Email"; // Heading Name
                    ws.Cells[RowNum, ColNum + 2, RowNum, ColNum + 2].Merge = true; //Merge columns start and end range
                    ws.Cells[RowNum, ColNum + 2, RowNum, ColNum + 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[RowNum, ColNum + 2, RowNum, ColNum + 2].Style.Fill.BackgroundColor.SetColor(Color.Black);
                    ws.Cells[RowNum, ColNum + 2, RowNum, ColNum + 2].Style.Font.Bold = true; //Font should be bold    
                    ws.Cells[RowNum, ColNum + 2, RowNum, ColNum + 2].Style.Font.Color.SetColor(Color.White);
                    ws.Cells[RowNum, ColNum + 2, RowNum, ColNum + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                    ws.Cells[RowNum, ColNum + 3].Value = "State"; // Heading Name
                    ws.Cells[RowNum, ColNum + 3, RowNum, ColNum + 3].Merge = true; //Merge columns start and end range
                    ws.Cells[RowNum, ColNum + 3, RowNum, ColNum + 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[RowNum, ColNum + 3, RowNum, ColNum + 3].Style.Fill.BackgroundColor.SetColor(Color.Black);
                    ws.Cells[RowNum, ColNum + 3, RowNum, ColNum + 3].Style.Font.Bold = true; //Font should be bold    
                    ws.Cells[RowNum, ColNum + 3, RowNum, ColNum + 3].Style.Font.Color.SetColor(Color.White);
                    ws.Cells[RowNum, ColNum + 3, RowNum, ColNum + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center

                    ws.Cells[RowNum, ColNum + 4].Value = "Company URL"; // Heading Name
                    ws.Cells[RowNum, ColNum + 4, RowNum, ColNum + 4].Merge = true; //Merge columns start and end range
                    ws.Cells[RowNum, ColNum + 4, RowNum, ColNum + 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[RowNum, ColNum + 4, RowNum, ColNum + 4].Style.Fill.BackgroundColor.SetColor(Color.Black);
                    ws.Cells[RowNum, ColNum + 4, RowNum, ColNum + 4].Style.Font.Bold = true; //Font should be bold    
                    ws.Cells[RowNum, ColNum + 4, RowNum, ColNum + 4].Style.Font.Color.SetColor(Color.White);
                    ws.Cells[RowNum, ColNum + 4, RowNum, ColNum + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center
                    RowNum++;

                    int i = 1;
                    foreach (var item in DBService.GetAllCompany())
                    {                        
                        ws.Cells[RowNum, ColNum].Value = i;
                        ws.Cells[RowNum, ColNum + 1].Value = item.CompanyName.Replace("&amp;", "&");
                        ws.Cells[RowNum, ColNum + 2].Value = (item.Email??"").ToLower();
                        ws.Cells[RowNum, ColNum + 3].Value = item.State;
                        ws.Cells[RowNum, ColNum + 4].Value = item.ComanyURL;
                        i++;
                        RowNum++;
                    }
                    ws.Column(1).Width = 5;
                    ws.Column(2).Width = 40;
                    ws.Column(3).Width = 40;
                    ws.Column(4).Width = 15;
                    ws.Column(5).Width = 40;
                    // save our new workbook and we are done!
                    package.Save();
                }
       }
    }
}
