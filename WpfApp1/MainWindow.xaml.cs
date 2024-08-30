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
using OxyPlot;
using OxyPlot.Series;
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
            List<string> styles = new List<string> { "DinoThemes", "TangerineTheme" };
            ThemesComboBox.SelectionChanged += ThemesComboBox_SelectionChanged;
            ThemesComboBox.ItemsSource = styles;
            ThemesComboBox.SelectedItem = "DinoThemes";
        }
        public string data_row {  get; set; }
        public Data data_Current { get; set; }
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

        private void ThemesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string style = ThemesComboBox.SelectedItem as string;
           var uri = new Uri(style + ".xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }


        private string _curStr = string.Empty;
        private void ExcelGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var item = ExcelGrid.SelectedItem;
            if (item is DataRowView view)
            {
                DataRow rw = view.Row;
                object[] rows = rw.ItemArray;
                string str = string.Join(", ", rows);
                _curStr = str;
                data_Current ??= new();
                data_Current.HeaderRow = MainWindowViewModel.Instance.RowsData;
                data_Current.SelectRow = str;
            }
        }

        private void ExcelGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            var item = ExcelGrid.SelectedItem;
            if (item is DataRowView view)
            {
                DataRow rw = view.Row;
                object[] rows = rw.ItemArray;
                string str = string.Join(", ", rows);
                if (str != _curStr && MainWindowViewModel.Instance.Rows != null &&  !string.IsNullOrEmpty(_curStr))
                {
                    var collect_ind = MainWindowViewModel.Instance.Rows.IndexOf(data_Current);
                    _curStr = string.Empty;
                    foreach (var items in MainWindowViewModel.Instance.Rows.Skip(collect_ind))
                    {
                        items.SelectRow = str;

                        Debug.WriteLine($"{items.HeaderRow} {items.SelectRow}");
                        break;
                    }
                }
            }
        }

        private void ExcelGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           var item = ExcelGrid.SelectedItem;
            var point = e.GetPosition(ExcelGrid);
            var hitTestResult = VisualTreeHelper.HitTest(ExcelGrid, point);
            if (hitTestResult == null)
            {
                return;
            }
            DataGridCell cell = FindVisualChild<DataGridCell>(hitTestResult.VisualHit);
            if(cell == null)
            {
                return;
            }
            DataGridColumn dataGridColumn = cell.Column;
            int col_index = dataGridColumn.DisplayIndex;
            DataGridRow dataGridRow = FindVisualChild<DataGridRow>(cell);
            if (dataGridRow == null)
            {
                return;
            }

            var row = dataGridRow.Item;
            var value = (row as DataRowView)?.Row[col_index].ToString();
           if(double.TryParse(value, out double data))
            {
                GraphModel model = new();
                DataPoint point1 = new DataPoint(data, data);
                model.AddPoints(point1);
            }
           

        }

        private static T FindVisualChild<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            DependencyObject dependencyObject1 = VisualTreeHelper.GetParent(dependencyObject);  
            if(dependencyObject1 == null)
            {
                return null;
            }
            T parent = dependencyObject as T;
            if(parent != null)
            {
                return parent;
            }
            return FindVisualChild<T>(dependencyObject1);
        }
        private void ExcelGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
           
        }
    }
}