using _8het.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8het.Entities
{
    public class CarFactory : ToyFactory
    {
        public Toy CreateNew()
        {
            Car car = new Car();
            return car;
        }
    }
}
