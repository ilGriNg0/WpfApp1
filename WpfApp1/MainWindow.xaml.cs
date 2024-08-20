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
        public string Data {  get; set; }
        public ObservableCollection<string> TextBlocks { get; set; }  = new ObservableCollection<string>();

        private void ExcelGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
         
          var item = ExcelGrid.SelectedItem;
          if(item is DataRowView view)
            {
                DataRow row = view.Row;
                object[] rowValues = row.ItemArray;
                string mess = string.Join(", ", rowValues);
               
                //ItemsRow.Items.Add(mess);
                //TextBlocks.Add(mess);
                Data = MainWindowViewModel.Instance.RowsData;
                //MainWindowViewModel.Instance.RowsData = mess;
                MainWindowViewModel.Instance.Rows ??= new();
                MainWindowViewModel.Instance.Rows.Add(mess);
                //;                txt.Text = mess;


                Debug.WriteLine(mess);
            }   
        }   
    }
}