using System;
using Lab1_Euler_Rynge_Kytt;

namespace Lab1_Euler_Rynge_Kytt
{
    public class SampleFunction : Function
    {

        public double A { get; set; }

        public double M
        {
            get;
            set;
        }

        public SampleFunction(double a, double m)
        {
            A = a;
            M = m;
        }
        public double GetSolve(double X, double Y)
        {
            return A * (1 - Y * Y) / ((1 + M) * X * X + Y * Y + 1);
        }
    }
}