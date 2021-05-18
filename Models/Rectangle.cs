using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Rectangle : Shape
    {
        public static Shape Create()
        {
            Random rnd = new Random();
            return new Rectangle() { 
                Longueur = (decimal)rnd.NextDouble()*10 ,
                Largeur = (decimal)rnd.NextDouble() * 10,
                Name = "Rectangle",
                Id = rnd.Next(1, 1000)
            };
        }
        
        public decimal Longueur { get; set; } 
        public decimal Largeur { get; set; } 

    }
}
