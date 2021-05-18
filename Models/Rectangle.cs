using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Rectangle : Shape
    {
        public Rectangle()
        {
            Random rnd = new Random();

            Longueur = (decimal)rnd.NextDouble() * 10;
            Largeur = (decimal)rnd.NextDouble() * 10;
            Name = "Rectangle";
            Id = rnd.Next(1, 1000);
        }


        public decimal Longueur { get; set; }
        public decimal Largeur { get; set; }

    }
}
