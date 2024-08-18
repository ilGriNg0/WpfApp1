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

        private void ExcelGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
         ObservableCollection<TextBlock> textBlocks = new ObservableCollection<TextBlock>();
         
          var item = ExcelGrid.SelectedItem;
          if(item is DataRowView view)
            {
                DataRow row = view.Row;
                object[] rowValues = row.ItemArray;
                string mess = string.Join(", ", rowValues);
                item_control.Items.Add(mess);
                Txt.Text = mess;
                textBlocks.Add(Txt);
                Debug.WriteLine(mess);
            }
            
            
        }

       
    }
}