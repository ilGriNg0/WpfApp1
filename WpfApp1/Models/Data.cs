using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Data : INotifyPropertyChanged
    {
        private string? _headerRow;
        private string? _selectRow;
        
        public string? HeaderRow
        {
            get{ return _headerRow; }
            set 
            {
                _headerRow = value;
                OnPropertyChanged("HeaderRow");
            }
        }
      
        public string? SelectRow
        {
            get { return _selectRow; }
            set 
            {
                _selectRow = value;
                OnPropertyChanged("SelectRow");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) { return false; }
            var item = obj as Data;
            return HeaderRow == item?.HeaderRow && SelectRow == item?.SelectRow;
        }

        public override int GetHashCode() => HashCode.Combine(HeaderRow, SelectRow);
    }
}
