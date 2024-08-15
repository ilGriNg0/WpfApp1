using ExcelDataReader;
using Microsoft.Win32;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml.Schema;
using WpfApp1.Models;

namespace WpfApp1
{
    partial class LoadDataExcel : ILoadData
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
        //   ReadExcelFiles(path);
           
        //}
        public DataTable ReadExcelFiles(string path)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
         
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var res = reader.AsDataSet();
                    return res.Tables.Count > 0 ? res.Tables[0] : null;

                  

                }
            }
         

        }
      
        
       
       
    }
}
