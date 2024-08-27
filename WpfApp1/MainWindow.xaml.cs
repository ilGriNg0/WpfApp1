﻿using System.Collections.ObjectModel;
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
            List<string> styles = new List<string> { "Themes", "TangerineTheme" };
            ThemesComboBox.SelectionChanged += ThemesComboBox_SelectionChanged;
            ThemesComboBox.ItemsSource = styles;
            ThemesComboBox.SelectedItem = "test";
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

        private void ThemesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string style = ThemesComboBox.SelectedItem as string;
           var uri = new Uri(style + ".xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

       

      

        private void ExcelGrid_CurrentCellChanged(object sender, EventArgs e)
        {

           
            var item = ExcelGrid.SelectedItem;
           if(item is DataRowView view)
            {
                DataRow rw = view.Row;
                object[] rows = rw.ItemArray;
                string str = string.Join(',', rows);

                foreach (var items in MainWindowViewModel.Instance.Rows)
                {
                    items.SelectRow = str;
                    Debug.WriteLine($"{items.HeaderRow} {items.SelectRow}");
                }

            }



         }
    }
}