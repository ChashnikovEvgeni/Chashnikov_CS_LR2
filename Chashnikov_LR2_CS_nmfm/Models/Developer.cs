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
       // [Required(ErrorMessage = "Укажите имя разработчика")]
        public string Name { get; set; }
       // [Range(1, 100, ErrorMessage = "Возраст должен быть в промежутке от 1 до 100")]
        //[Required(ErrorMessage = "Укажите возраст разработчика")]
        public int Age { get; set; }
        public string Company { get; set; }

       public List<Application> Applications { get; set; }
        //public Developer()
        //{
          //  Applications = new List<Application>();
        //}

    }
}
