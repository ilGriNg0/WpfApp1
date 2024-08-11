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
        private string? _name;
        private int _distance;
        private int _angel;
        private float _width;
        private float _height;
        private string? _isDefect;
        
        public string? Name
        {
            get{ return _name; }
            set 
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public int Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                OnPropertyChanged("Distance");
            }
        }
        public int Angel
        {
            get { return _angel; }
            set
            {
                _angel = value;
                OnPropertyChanged("Angel");
            }
        }
        public float Width
        {
            get {return _width; }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
        }
        public float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
            }
        }
        public string? IsDefect
        {
            get { return _isDefect; }
            set 
            {
                _isDefect = value;
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
