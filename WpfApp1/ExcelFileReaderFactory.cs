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
        public static ILoadData GetLoadData(string path)
        {
            string extension = Path.GetExtension(path).ToLower();
            switch (extension)
            {
                case ".csv":

                    return new LoadDataCsv();
                case ".xls":
                case ".xlsx":
                    return new LoadDataExcel();
                default:
                    throw new NotSupportedException($"Extension : {extension} not correct");
            }

        }
    }
}
