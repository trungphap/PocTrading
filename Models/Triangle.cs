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

            A = rnd.Next(1, int.MaxValue);
            B = rnd.Next(1, int.MaxValue);
            C = rnd.Next(1, int.MaxValue);
            Name = "Triangle";
            Id = rnd.Next(1, 1000);
        }


        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public override string ToString() => $"A = {A} B = {B} C = {C}";

    }
}
