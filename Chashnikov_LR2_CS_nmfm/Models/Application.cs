using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Chashnikov_LR2_CS.Models
{
    public class Application
    {
        public long Id { get; set; }

      //  [Required(ErrorMessage = "Укажите название приложения")]
        public string Name { get; set; }
     //   [Required(ErrorMessage = "Укажите назначение программы")]
        public string Appointment { get; set; }
        public long DeveloperId { get; set; }
        public Developer Developer { get; set; }






       //  public virtual List<User> Users { get; set; }

      //  public List<User> Users { get; set; }
        //public Application()
        //{
        //    Users = new List<User>();
        //}
    }

   

}

