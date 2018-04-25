
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
            Type boxType = typeof(Box);
            FieldInfo[] fields = boxType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            try
            {
                var box = new Box();
                box.Length = double.Parse(Console.ReadLine());
                box.Width = double.Parse(Console.ReadLine());
                box.Height = double.Parse(Console.ReadLine());

                Console.WriteLine(fields.Count());
                Box.SurfaceArea(box);
                Box.LateralSurfaceArea(box);
                Box.Volume(box);
            }
            catch (Exception exc)
            {
                Console.WriteLine(fields.Count());
                Console.WriteLine(exc.Message);
            }

        }
    }
}
