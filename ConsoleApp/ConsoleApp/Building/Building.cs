using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public abstract class Building
    {
        public List<Floor> Floors { get; protected set; }

        protected void Initialize()
        {
            this.Floors = new List<Floor>();
        }

        protected void AddFloors(int noOfFloors)
        {
            for(var i = 0; i < noOfFloors; i++)
            {
                this.Floors.Add(new Floor() { Number = i });
            }
        }
    }
}
