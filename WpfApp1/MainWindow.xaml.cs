using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel.Instance ;
        }
        public string data_row {  get; set; }

        private void ExcelGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
         
          var item = ExcelGrid.SelectedItem;
          if(item is DataRowView view)
            {
                DataRow row = view.Row;
                object[] rowValues = row.ItemArray;
                string mess = string.Join(", ", rowValues);
                data_row = MainWindowViewModel.Instance.RowsData;
                Data data = new();
                data.HeaderRow = data_row;
                data.SelectRow = mess;
                MainWindowViewModel.Instance.SetObservable(data);
                Debug.WriteLine(mess);
            }   
        }   
    }
}