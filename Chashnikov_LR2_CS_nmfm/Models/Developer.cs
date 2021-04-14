using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Chashnikov_LR2_CS.Models
{
    public class Developer
    {
        public long Id { get; set; }
        public string Name { get; set; }
      
        public int Age { get; set; }
        public string Company { get; set; }

        public List<Application> Applications { get; set; } = new List<Application>();
       
 }
}
