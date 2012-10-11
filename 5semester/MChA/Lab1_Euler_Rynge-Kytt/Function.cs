using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1_Euler_Rynge_Kytt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public interface Function
    {
        double A { get; set; }
        double M { get; set; }
        double GetSolve(double X, double Y);
    }
}
