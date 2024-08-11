using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    Debug.WriteLine(reader.FieldCount);
                    var configuration = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = false        
                        }
                    };
                     reader.AsDataSet(configuration);
                  Debug.WriteLine(reader.FieldCount);

                       
                        do
                        {
                            while (reader.Read())
                            {
                                datas.Add(new Data

                                {
                                    Name = reader.GetString(1),
                                    //Distance = reader.GetDouble(1),
                                    //Angel = reader.GetInt32(2),
                                    //Width = reader.GetDouble(3),
                                    //Height = reader.GetDouble(4),
                                    //IsDefect = reader.GetString(5),

                                });



                            }
                        } while (reader.NextResult());

                        // 2. Use the AsDataSet extension method


                        //The result of each spreadsheet is in result.Tables
                    }
            }
        }
    }
}
