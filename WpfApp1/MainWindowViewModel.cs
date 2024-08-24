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
using System.Windows;
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

        private bool _machine;
        public bool Machine
        {
            get => _machine;

            set
            {
                _machine = value;
                OnPropertyChanged("Machine"); 
            }
        }

        private string _rowsData;
        public string RowsData
        {
            get { return _rowsData; }

            set
            {
                _rowsData = value;
                OnPropertyChanged("RowsData");
            }
        }
        private ObservableCollection<Data> _rows;

        public ObservableCollection<Data> Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                OnPropertyChanged("Rows");
            }
        }
        ILoadData loadData { get; set; }
        IWriteExcel writeExcel { get; set; }
        IMachineState state { get; set; }
        public MainWindowViewModel(ILoadData loadData, IWriteExcel writeExcel, IMachineState state)
        {
            this.loadData = loadData;
            this.writeExcel = writeExcel;
            this.state = state;
            Machine = state.SelectState(0);
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
                            Machine = state.SelectState(1);
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

        private RelayCommand _saveTable;

        public RelayCommand SaveTable
        {
            get {
                return _saveTable ??= new RelayCommand(async () =>
            {
                try
                {
                   await writeExcel.WriteDataToExcel(path,Table);
                }
                catch (Exception)
                {

                    throw;
                }

            }); }
        }

        private RelayCommand<object> _deleteRows;

        public RelayCommand<object> DeleteRows
        {
            get {
                return _deleteRows ??= new RelayCommand<object>((param) =>
            {
                try
                {
                    if(param is Data item)
                    {
                        int i = Rows.IndexOf(item);
                        Rows.RemoveAt(i);
                    }
                   
                }
                catch (Exception)
                {

                    throw;
                }
            });
            }
           
        }

        private RelayCommand _saveExcelFiles;
        public RelayCommand SaveExcelFiles
        {
            get { return _saveExcelFiles ??= new RelayCommand(async() =>
            {
                try
                {
                    SaveFileDialog saveFileDialog = new()
                    {
                        Filter = $"Excel files| *{ExcelFileReaderFactory.Extension}"
                    };
                    if(saveFileDialog.ShowDialog() == true)
                    {
                        path =saveFileDialog.FileName;
                        await writeExcel.WriteDataToExcel(path, Table);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }); }
        }
        internal void SetObservable(Data strings)
        {
            Rows ??= new();
            Rows.Add(strings);
        }
        public static MainWindowViewModel Instance
        {
            get { if (_instance == null)
                {
                    _instance = new MainWindowViewModel(new LoadDataExcel(), new WriteExcel(), new MachineState());
                }
            return _instance;
            }
        }
       

      
    }
}
