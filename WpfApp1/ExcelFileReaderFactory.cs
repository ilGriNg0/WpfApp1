using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ExcelFileReaderFactory
    {
        public static string? Extension {get; set;}
        public static ILoadData GetLoadData(string path)
        {
            Extension = Path.GetExtension(path).ToLower();
            switch (Extension)
            {
                case ".csv":

                    return new LoadDataCsv();
                case ".xls":
                case ".xlsx":
                    return new LoadDataExcel();
                case ".xlsb":
                    return new LoadDataExcelBinary();
                default:
                    throw new NotSupportedException($"Extension : {Extension} not correct");
            }

        }
    }
}
