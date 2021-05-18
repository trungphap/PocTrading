using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Circle : Shape
    {
        public Circle()
        {
            Random rnd = new Random();
            Diametre = (decimal)rnd.NextDouble() * 10;
            Name = "Circle";
            Id = rnd.Next(1, 1000);
        }

        public decimal Diametre { get; set; }
    }
}
