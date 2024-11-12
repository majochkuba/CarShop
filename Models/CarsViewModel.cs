using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Models.Database;

namespace CarShop.Models
{
    public class CarsViewModel
    {
        public List<Car> Cars { get; set; }
        
        public VechiclesModel VechiclesFromApi { get; set; }
        
        
    }
}