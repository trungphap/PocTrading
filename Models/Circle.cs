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
            Diametre = rnd.Next(1, int.MaxValue);
            Name = "Circle";
            Id = rnd.Next(1, 1000);
        }

        public int Diametre { get; set; }
        public override string ToString() => $"Diametre = {Diametre}";
    }
}
