using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class HighRiseBuilding : ResidentialApartment
    {
        public HighRiseBuilding(int noOfFloors)
        {
            this.AddFloors(noOfFloors);
        }
    }
}
