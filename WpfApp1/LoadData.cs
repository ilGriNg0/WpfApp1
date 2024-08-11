using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Xml.Schema;
using WpfApp1.Models;

namespace WpfApp1
{
    partial class LoadData : ILoadData
    {
        ObservableCollection<Data> datas = new ObservableCollection<Data>();
        
        public void ReadXLS(string path)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Choose one of either 1 or 2:

                    // 1. Use the reader methods
                    do
                    {
                        while (reader.Read())
                        {
                            datas.Add(new Data
                            
                        {
                             Name = reader.GetString(0),
                             Distance  = reader.GetInt32(1),
                             Angel  = reader.GetInt32(2),
                             Width = reader.GetFloat(3),
                             Height =  reader.GetFloat(4),
                             IsDefect = reader.GetString(5),

                        });
                            
                          
                              
                        }
                    } while (reader.NextResult());

                    // 2. Use the AsDataSet extension method
                    //var result = reader.AsDataSet();

                    // The result of each spreadsheet is in result.Tables
                }
            }
        }
    }
}
