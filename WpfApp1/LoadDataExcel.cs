﻿using ExcelDataReader;
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
         

        }
        //private byte[] ReadBite(Stream s, int size)
        //{
        //    var mas = new byte[size];
        //    int n = size;
        //    while (n > 0)
        //    {
        //        var buf = s.Read(mas);
        //        if(n == 0)
        //        {
        //            throw new FileFormatException("EOF");
        //        }
        //        n--;

        //    }
        //    string str = Encoding.UTF8.GetString(mas);  
        //    Debug.WriteLine(str);
        //    return mas;
        //}
      
        
       
       
    }
}
