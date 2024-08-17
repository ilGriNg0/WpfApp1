using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Resources;
using WpfApp1.Models;
namespace WpfApp1
{
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
 

        private DataSet _dataset;

        private DataTable _table;

        public DataSet SetData
        {
            get
            {
                return _dataset;
            }
            set
            {
                _dataset = value;
                OnPropertyChanged("SetData");
            }
        }

        public DataTable Table
        {
            get { return _table; }
            set
            {
                _table = value;
                OnPropertyChanged("Table");
            }
        }
        ILoadData loadData { get; set; }

        public MainWindowViewModel(ILoadData loadData)
        {
            this.loadData = loadData;
        }

        private static MainWindowViewModel? _instance;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CanExecuteChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        string path {  get; set; }
        private RelayCommand _dialogopen;
        public RelayCommand DialogOpen
        {
            get
            {
                return _dialogopen ?? (_dialogopen = new RelayCommand(async () =>
                {
                    try
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog
                        {
                            Filter = "Excel files| *.xls; *.xlsx; *xlsb|Csv files|*.csv"
                        };
                        if (openFileDialog.ShowDialog() == true)
                        {
                            path = openFileDialog.FileName;
                            var reader = ExcelFileReaderFactory.GetLoadData(path);  
                            Table = await reader.ReadExcelFiles(path);

                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }));
            }
        }

        private RelayCommand _lazyLoad;

        public RelayCommand LazyLoad
        {
           get
            {
                return _lazyLoad ??= new RelayCommand(async () =>
                {
                    try
                    {
                        var reader = ExcelFileReaderFactory.GetLoadData(path);
                        Table = await reader.lazyTable(Table, path, Table.Rows.Count, CountRows.GetRowsCount, 50);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                });
            }
        }
        public static MainWindowViewModel Instance
        {
            get { if (_instance == null)
                {
                    _instance = new MainWindowViewModel(new LoadDataExcel());
                }
            return _instance;
            }
        }
       

      
    }
}
