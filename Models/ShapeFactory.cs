using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ShapeFactory
    {
        public delegate Shape Creator();
        static public void Register(int shapeType, Creator creator)
        {
            // check if key exist
            Creators.Add(shapeType, creator);
        }
        static public Shape Create(int shapeType)
        { 
            if (Creators.ContainsKey(shapeType))
            {
               return Creators[shapeType]();
            }
            return null;
        }
        public static Dictionary<int, Creator> Creators = new Dictionary<int, Creator>();
    }
}
