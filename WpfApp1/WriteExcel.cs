using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class WriteExcel : IWriteExcel
    {
        public Task WriteDataToExcel(string path, DataTable table)
        {
            return Task.Run(() =>
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    ExcelWorksheet excelWorksheet = package.Workbook.Worksheets.Add(path);
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        excelWorksheet.Cells[1, i + 1].Value = table.Columns[i].ColumnName;
                    }
                  
                    for (int row = 0; row < table.Rows.Count; row++)
                    {
                        for (int col = 0; col < table.Columns.Count; col++)
                        {
                            var dt = table.Rows[row][col].ToString();
                            if (double.TryParse(dt,  out double data))
                            {
                                excelWorksheet.Cells[row + 2, col + 1].Value =data;
                                Debug.WriteLine(data);
                            }
                            else
                            {
                                excelWorksheet.Cells[row + 2, col + 1].Value = table.Rows[row][col];
                            }
                           
                        }
                    }

                    excelWorksheet.Cells[excelWorksheet.Dimension.Address].AutoFitColumns();
                    FileInfo fileInfo = new FileInfo(path);
                    package.SaveAs(fileInfo);
                }
            });


        }
    }
}
