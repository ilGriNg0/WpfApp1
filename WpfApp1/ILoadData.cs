﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public interface ILoadData
    {
        public void ReadXLS(string path);
    }
}
