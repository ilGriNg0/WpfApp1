using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
namespace WpfApp1
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Data>? _datas;

        private string path = "C:\\Users\\User\\source\\repos\\WpfApp1\\WpfApp1\\bin\\Debug\\net8.0-windows\\Objects.xlsx";
        public ObservableCollection<Data> Datas
        {
            get
            {
                return _datas;
            }
            set
            {
                _datas = value;
                OnPropertyChanged("Datas");
            }
        }
        ILoadData loadData { get; set; }

     

       public MainWindowViewModel(ILoadData loadData)
        {
            Datas = new ObservableCollection<Data>();
            this.loadData = loadData;
            loadData.ReadXLS(path);
        }

        private static MainWindowViewModel? _instance;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public static MainWindowViewModel Instance
        {
            get { if (_instance == null)
                {
                    _instance = new MainWindowViewModel(new LoadData());
                }
            return _instance;
            }
        }

      
    }
}
