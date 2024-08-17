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



        public Task<DataTable> ReadExcelFiles(string path)
        {

            //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            //using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            //{
            //    var configuration = new ExcelDataSetConfiguration
            //    {
            //        ConfigureDataTable = _ => new ExcelDataTableConfiguration
            //        {
            //            UseHeaderRow = true
            //        }
            //    };
            //    using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
            //    {
            //        var res = reader.AsDataSet(configuration);
            //        return res.Tables.Count > 0 ? res.Tables[0] : null;
            //    }
            //}
            throw new NotImplementedException();

        }

        public Task<DataTable> lazyTable(DataTable dataTable, string path, int current_row, int max_row, int increase)
        {
            //using (var open_file = new FileStream(path, FileMode.Open, FileAccess.Read))
            //{

            //    using (var reader = ExcelReaderFactory.CreateReader(open_file))
            //    {

            //        int cnt = 0;
            //        while (reader.Read() && cnt < max_row)
            //        {
            //            cnt++;

            //            if (current_row > cnt)
            //            {
            //                continue;
            //            }
            //            else
            //            {
            //                DataRow row = dataTable.NewRow();
            //                for (int i = 0; i < reader.FieldCount; i++)
            //                {
            //                    row[i] = reader.GetValue(i);
            //                }
            //                dataTable.Rows.Add(row);

            //            }

            //        }
            //        return dataTable;
            //    }
            //}
            throw new NotImplementedException();
        }

       
    }
}
