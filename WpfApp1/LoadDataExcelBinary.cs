using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class LoadDataExcelBinary : ILoadData
    {
        public DataTable ReadExcelFiles(string path)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read) )
            {
                using (var reader =  ExcelReaderFactory.CreateBinaryReader(stream))
                {
                    var configuration = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };
                    var excel = reader.AsDataSet(configuration);
                    return excel.Tables.Count > 0 ? excel.Tables[0] : null;
                }
            }
        }
    }
}
