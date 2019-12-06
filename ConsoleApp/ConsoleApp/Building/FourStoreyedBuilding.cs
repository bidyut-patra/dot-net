using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class FourStoreyedBuilding : ResidentialApartment
    {  
        public FourStoreyedBuilding()
        {
            this.AddFloors(4);
        }
    }
}
