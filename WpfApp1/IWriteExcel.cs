using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public interface IWriteExcel
    {
        public Task WriteDataToExcel(string path, DataTable table);
    }
}
