using ExcelDataReader;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class LoadDataCsv : ILoadData
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
                        //var res = reader.AsDataSet(configuration);
                        //var table = res.Tables[0];
                        bool firstrow = true;
                        int maxCnt = 50;
                        while (reader.Read() && maxCnt > 0)
                        {
                            if (firstrow)
                            {
                                // Чтение заголовков
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    dataTable.Columns.Add(reader.GetString(i));
                                }
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


                        //table.Clear();
                        //res.Tables.Clear();
                        //res.Tables.Add(dataTable);
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

                            if (current_row > cnt)
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
