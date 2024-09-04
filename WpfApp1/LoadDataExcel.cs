using ExcelDataReader;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Schema;
using WpfApp1.Models;

namespace WpfApp1
{
    partial class LoadDataExcel : ILoadData
    {

        public Task<DataTable> ReadExcelFiles(string path)
        {
            return Task.Run(() =>
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                DataTable dataTable = new DataTable();
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {

                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var configuration = new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true
                            }
                        };
                        CountRows.GetRowsCount = reader.RowCount;
                        bool firstrow = true;
                        int maxCnt = 50;
                        while (reader.Read() && maxCnt > 0)
                        {
                            if (firstrow)
                            {
                                // Чтение заголовков
                                string[] mass = [];
                                List<string> list = new List<string>(); 
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var item = reader.GetValue(i).ToString();
                                    dataTable.Columns.Add(item);
                                    list.Add(item);   
                                }
                                string teste = string.Join(", ", list);
                                MainWindowViewModel.Instance.RowsData = teste;
                                firstrow = false;
                            }
                            else
                            {
                                // Чтение строк данных
                                DataRow row = dataTable.NewRow();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[i] = reader.GetValue(i);
                                }
                                dataTable.Rows.Add(row);

                            }
                            maxCnt--;

                        }

                        return dataTable;
                    }
                }

            });
            
         

        }

        public Task<DataTable> lazyTable(DataTable dataTable, string path, int current_row, int max_row, int increase)
        {
            return Task.Run(() =>
            {
                using (var open_file = new FileStream(path, FileMode.Open, FileAccess.Read))
                {

                    using (var reader = ExcelReaderFactory.CreateReader(open_file))
                    {

                        int cnt = 0;
                        int rows_add = 0;
                        while (reader.Read() && cnt < max_row && rows_add < increase)
                        {
                            cnt++;

                            if (current_row >= cnt)
                            {
                                continue;
                            }
                            else
                            {
                                DataRow row = dataTable.NewRow();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[i] = reader.GetValue(i);
                                }
                               
                                App.Current.Dispatcher.Invoke(() =>
                                {
                                    dataTable.Rows.Add(row);
                                });
                                rows_add++;
                            }

                        }
                       
                        return dataTable;
                    }
                }

            });
        }
    }
}
