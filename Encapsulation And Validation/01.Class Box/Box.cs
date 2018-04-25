using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENCAPSULATION_AND_VALIDATION
{
    public class Box
    {
        private double length { get; set; }
        private double width { get; set; }
        private double height { get; set; }


        public double Length
        {
            get
            {
                return this.length;
            }

            set
            {
                if(value > 0)
                {
                    this.length = value;
                }
            }
        }

        public double Width
        {
            get
            {
                return this.width;
            }

            set
            {
                if(value > 0)
                {
                    this.width = value;
                }
            }
        }

        public double Height
        {
            get
            {
                return this.height;
            }

            set
            {
                if(value > 0)
                {
                    this.height = value;
                }
            }
        }


        public static void SurfaceArea(Box box)
        {
            var surfaceArea = (2 * box.length * box.width) + (2 * box.length * box.height) + (2 * box.width * box.height);
            Console.WriteLine($"Surface Area - {surfaceArea:f2}");
        }

        public static void Volume(Box box)
        {
            var volume = box.length * box.width * box.height;
            Console.WriteLine($"Volume - {volume:f2}");
        }

        public static void LateralSurfaceArea(Box box)
        {
            var lateralArea = (2 * box.length * box.height) + (2 * box.width * box.height);
            Console.WriteLine($"Lateral Surface Area - {lateralArea:f2}");
        }
    }
}
