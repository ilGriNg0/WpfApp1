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
        public DataTable lazyTable(DataTable dataTable, string path, int current_row, int max_row)
        {
            throw new NotImplementedException();
        }

        //public string path { get; set; }
        //public string path_extension { get; set; }
        //public void DialogOpen()
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    if (openFileDialog.ShowDialog() == true)
        //    {
        //        path = openFileDialog.FileName;
        //    }
        //    ReadExcelFiles(path);

        //}



        public DataTable ReadExcelFiles(string path)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                var configuration = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    var res = reader.AsDataSet(configuration);
                    return res.Tables.Count > 0 ? res.Tables[0] : null;
                }
            }

        }
    }
}
