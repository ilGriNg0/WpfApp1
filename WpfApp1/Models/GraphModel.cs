using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using OxyPlot;
using OxyPlot.Series;
namespace WpfApp1.Models
{
    public class GraphModel : INotifyPropertyChanged
    {
        private static LineSeries _points;

        public LineSeries Points_data
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
                OnPropertyChanged("Points");
            }
        }

        private static GraphModel _instance;

        public static GraphModel Instance
        {
           get
            { 
                if (_instance == null)
                {
                    _instance = new GraphModel();    
                }
                return _instance;
            }
        }

        //private PlotModel _plotModelGraphs;

        //public PlotModel PlotModelGraphs
        //{
        //    get { return _plotModelGraphs; }
        //    set { _plotModelGraphs = value; OnPropertyChanged("PlotModelGraphs"); }
        //}
        public void ParamsGraphs()
        {
            //Points ??= new List<DataPoint> { new DataPoint() };
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public LineSeries AddPoints(DataPoint point)
        {
            Points_data ??= new LineSeries();
            Points_data.Points.Add(point);
            MainWindowViewModel.Instance.PlotModelGraphs ??= new PlotModel();
            MainWindowViewModel.Instance.PlotModelGraphs.Title = "ssss";
            MainWindowViewModel.Instance.PlotModelGraphs.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1));
            return Points_data;
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
