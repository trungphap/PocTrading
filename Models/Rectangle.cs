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

            Longueur = rnd.Next(1,int.MaxValue);
            Largeur = rnd.Next(1, int.MaxValue);
            Name = "Rectangle";
            Id = rnd.Next(1, 1000);
        }


        public int Longueur { get; set; }
        public int Largeur { get; set; }
        public override string ToString() => $"Longueur = {Longueur} Largeur = {Largeur}";
    }
}
