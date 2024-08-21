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
                OnPropertyChanged("IsDefect");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
