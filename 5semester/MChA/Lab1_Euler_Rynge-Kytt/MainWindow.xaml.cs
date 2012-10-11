using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System.Threading;
using System.Globalization;

namespace Lab1_Euler_Rynge_Kytt
{
    public partial class MainWindow : Window
    {

        private List<LineGraph> graphs;
        private List<List<string>> values;
        private int currentValues;
        public MainWindow()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            graphs = new List<LineGraph>();
            values = new List<List<string>>();
        }

        private InputData GetInputData()
        {
            return new InputData
            {
                A = Double.Parse(ATextBox.Text),
                H = Double.Parse(HTextBox.Text),
                LeftLimit = double.Parse(LeftLimitTextBox.Text),
                RightLimit = double.Parse(RightLimitTextBox.Text),
                M = double.Parse(MTextBox.Text),
                X0 = double.Parse(X0TextBox.Text),
                Y0 = double.Parse(Y0TextBox.Text)
            };
        }

        private void EulerButton_Click(object sender, RoutedEventArgs e)
        {
            var data = GetInputData();
            List<double> x;
            List<double> y;

            Function func = new SampleFunction(data.A, data.M);

            GetEulerSolves(func, data, out x, out y);
            DrawGraphic(x.ToArray(), y.ToArray(), Colors.Green, "Эйлер", 1);
            FillValueListBox(x, y);
        }

        private void GetEulerSolves(Function func, InputData data, out List<double> x, out List<double> y)
        {
            double curX = data.X0;
            double curY = data.Y0;
            x = new List<double>();
            y = new List<double>();

            while (Math.Round(curX, 10) <= data.RightLimit + data.H)
            {
                y.Add(curY);
                x.Add(curX);

                curY = y[y.Count - 1] + data.H * func.GetSolve(x[x.Count - 1], y[y.Count - 1]);

                curX += data.H;
            }
        }

        public void DrawGraphic(double[] x, double[] y, Color color, string legend, double thickness = 3)
        {
            // Create data sources:
            var xDataSource = x.AsXDataSource();
            var yDataSource = y.AsYDataSource();

            CompositeDataSource compositeDataSource = xDataSource.Join(yDataSource);
            // adding graph to plotter
            graphs.Add(plotter.AddLineGraph(compositeDataSource,
                color,
                thickness,
                legend));

            // Force evertyhing plotted to be visible
            plotter.FitToView();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var graph in graphs)
            {
                graph.Remove();
            }
            graphs = new List<LineGraph>();
        }

        private void GetRungeKytSolves(Function func, InputData data, out List<double> x, out List<double> y)
        {
            double curX = data.X0;
            double curY = data.Y0;
            double h = data.H;

            x = new List<double>();
            y = new List<double>();

            while (Math.Round(curX, 10) <= data.RightLimit + h)
            {
                y.Add(curY);
                x.Add(curX);
                double K1 = h * func.GetSolve(curX, curY);
                double K2 = h * func.GetSolve(curX + h / 2, curY + K1 / 2);
                double K3 = h * func.GetSolve(curX + h / 2, curY + K2 / 2);
                double K4 = h * func.GetSolve(curX + h, curY + K3);
                curY = curY + 1f / 6f * (K1 + 2 * K2 + 2 * K3 + K4);
                curX += data.H;
            }
        }

        private void KytButton_Click(object sender, RoutedEventArgs e)
        {

            var data = GetInputData();
            List<double> x;
            List<double> y;

            Function func = new SampleFunction(data.A, data.M);
            GetRungeKytSolves(func, data, out x, out y);
            DrawGraphic(x.ToArray(), y.ToArray(), Colors.Red, "Рунге-Кутт", 1);
            FillValueListBox(x, y);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (graphs.Count != 0)
            {
                graphs[graphs.Count - 1].Remove();
                graphs = graphs.Take(graphs.Count - 1).ToList<LineGraph>();
            }
            if (currentValues == values.Count - 1)
            {
                currentValues--;
                DrawCurrentValues();
            }
            values.Remove(values[values.Count-1]);
        }

        private void EulerMButton_Click(object sender, RoutedEventArgs e)
        {
            var data = GetInputData();
            List<double> x;
            List<double> y;

            Function func = new SampleFunction(data.A, data.M);
            GetEulerMSolves(func, data, out x, out y);
            DrawGraphic(x.ToArray(), y.ToArray(), Colors.Blue, "Эйлер модиф.", 1);
            FillValueListBox(x, y);
        }

        private void GetEulerMSolves(Function func, InputData data, out List<double> x, out List<double> y)
        {
            double curX = data.X0;
            double curY = data.Y0;
            x = new List<double>();
            y = new List<double>();

            while (Math.Round(curX, 10) <= data.RightLimit + data.H)
            {
                x.Add(curX);
                y.Add(curY);

                curY = curY + data.H * func.GetSolve(curX + data.H / 2,
                    curY + (data.H / 2) * func.GetSolve(curX, curY));

                curX += data.H;

            }
        }

        private void AdamsButton_Click(object sender, RoutedEventArgs e)
        {
            var data = GetInputData();
            List<double> x;
            List<double> y;
            Function func = new SampleFunction(data.A, data.M);

            GetAdamsSolves(func, data, out x, out y);
            DrawGraphic(x.ToArray(), y.ToArray(), Colors.Gold, "Адамса", 1.5);
            FillValueListBox(x, y);

        }

        private void GetAdamsSolves(Function func, InputData data, out List<double> x, out List<double> y)
        {
            double curX = data.X0;
            double curY = data.Y0;
            x = new List<double>();
            y = new List<double>();
            x.Add(curX);
            y.Add(curY);

            double curPrevY = curY;
            double curPrevX = curX;

            double h = Math.Pow(data.H, 2);
            //Calculate next y and x
            double K1 = h * func.GetSolve(curX, curY);
            double K2 = h * func.GetSolve(curX + h / 2, curY + K1 / 2);
            double K3 = h * func.GetSolve(curX + h / 2, curY + K2 / 2);
            double K4 = h * func.GetSolve(curX + h, curY + K3);
            curY = curY + 1f / 6f * (K1 + 2 * K2 + 2 * K3 + K4);
            curX += h;
            //--------------------------------
            h = data.H-h;
            bool changeH = true;

            while (Math.Round(curX, 10) <= data.RightLimit + h)
            {
                x.Add(curX);
                y.Add(curY);


                curY = curY + h * ((3f / 2) * func.GetSolve(curX, curY) - 0.5 * func.GetSolve(curPrevX, curPrevY));
                curX += h;

                curPrevX = x[x.Count - 1];
                curPrevY = y[y.Count - 1];
                if (changeH)
                {
                    h = data.H;
                    changeH = false;
                }
            }
        }
        private void FillValueListBox(List<double> x, List<double> y)
        {
            List<string> items = new List<string>();
            for (int i = 0; i < x.Count; i++)
            {
                items.Add(String.Format("{0} - {1}", x[i], y[i]));
            }

            values.Add(items);
            currentValues = values.Count - 1;
            DrawCurrentValues();
        }

        private void DrawCurrentValues()
        {
            ValuesListBox.Items.Clear();
            if ((currentValues >= 0) && (currentValues <= values.Count))
            {
                GraphNumberLabel.Content = String.Format("График №:{0}", currentValues+1);
                foreach (var item in values[currentValues])
                {
                    ValuesListBox.Items.Add(item);
                }
            }
        }

        private void NextButton2_Click(object sender, RoutedEventArgs e)
        {
            if (currentValues < values.Count - 1)
                currentValues++;
            DrawCurrentValues();
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentValues > 0)
            {
                currentValues--;
            }
            DrawCurrentValues();
        }
    }
}
