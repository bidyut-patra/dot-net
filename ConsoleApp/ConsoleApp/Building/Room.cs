using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Room
    {
        public List<Window> Windows { get; set; }

        public Room()
        {
            this.Windows = new List<Window>();
        }
    }
}
