using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Floor
    {
        public int Number { get; set; }
        public List<Room> Rooms { get; protected set; }

        public Floor()
        {
            this.Rooms = new List<Room>();
        }
    }
}
