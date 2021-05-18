using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TriangleFactory : ShapeFactory
    {
        public override Shape CreateShape()
        {
            return new Triangle();
        }
    }
}
