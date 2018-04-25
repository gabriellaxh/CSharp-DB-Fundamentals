using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Engine
{
    public int EngineSpeed { get; set; }
    public int EnginePower { get; set; }

    public Engine(int engineSpeed, int enginePower)
    {
        this.EngineSpeed = engineSpeed;
        this.EnginePower = enginePower;
    }
}
