using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Tire
{
    public double Tire1Pressure { get; set; }
    public int Tire1Age { get; set; }
    public double Tire2Pressure { get; set; }
    public int Tire2Age { get; set; }
    public double Tire3Pressure { get; set; }
    public int Tire3Age { get; set; }
    public double Tire4Pressure { get; set; }
    public int Tire4Age { get; set; }

    public Tire(double t1p,int t1a, double t2p, int t2a, double t3p, int t3a, double t4p, int t4a)
    {
        this.Tire1Pressure = t1p;
        this.Tire1Age = t1a;

        this.Tire2Pressure = t2p;
        this.Tire2Age = t2a;

        this.Tire3Pressure = t3p;
        this.Tire3Age = t3a;

        this.Tire4Pressure = t4p;
        this.Tire4Age = t4a;
    }
}


