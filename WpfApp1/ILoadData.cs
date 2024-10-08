﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1
{
    public interface ILoadData
    {
        public Task<DataTable> ReadExcelFiles(string path);
        public Task<DataTable> lazyTable(DataTable dataTable, string path, int current_row, int max_row, int increase);
    }
}
