using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Cargo
{
    public int CargoWeight { get; set; }
    public string CargoType { get; set; }

    public Cargo(int cargoWeight, string cargoType)
    {
        this.CargoWeight = cargoWeight;
        this.CargoType = cargoType;
    }
}

