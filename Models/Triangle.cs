using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Triangle : Shape
    {
        public Triangle()
        {
            Random rnd = new Random();

            A = (decimal)rnd.NextDouble() * 10;
            B = (decimal)rnd.NextDouble() * 10;
            C = (decimal)rnd.NextDouble() * 10;
            Name = "Triangle";
            Id = rnd.Next(1, 1000);
        }


        public decimal A { get; set; }
        public decimal B { get; set; }
        public decimal C { get; set; }

    }
}
