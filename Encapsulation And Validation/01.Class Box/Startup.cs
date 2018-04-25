
using ENCAPSULATION_AND_VALIDATION;
using System;
using System.Linq;
using System.Reflection;

namespace ClassBox
{
    public class Startup
    {
        static void Main(string[] args)
        {
            var box = new Box();
            box.Length = double.Parse(Console.ReadLine());
            box.Width = double.Parse(Console.ReadLine());
            box.Height = double.Parse(Console.ReadLine());

            Type boxType = typeof(Box);
            FieldInfo[] fields = boxType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            Console.WriteLine(fields.Count());

            Box.SurfaceArea(box);
            Box.LateralSurfaceArea(box);
            Box.Volume(box);

            
        }

        
    }
}
