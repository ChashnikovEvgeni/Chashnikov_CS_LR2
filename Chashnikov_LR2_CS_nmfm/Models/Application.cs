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
        public string Name { get; set; }
        public string Appointment { get; set; }
        public long Rating { get; set; }
       
        public long DeveloperId { get; set; }
        public Developer Developer { get; set; }
        public List<UsersApps> UsersApps { get; set; } = new List<UsersApps>();


        public long NumberOfUser
        {

            get
            {
                return UsersApps.Count();
                
            }
                
                   
        }

        public AppsInfo GetAppsInfo()
        {
            AppsInfo appsInfo = new AppsInfo();
            appsInfo.Id = Id;
            appsInfo.Name = Name;
            appsInfo.Appointment = Appointment;
            appsInfo.Rating = Rating;
            appsInfo.DeveloperId = DeveloperId;
            appsInfo.NumberOfUser = NumberOfUser;
            return appsInfo;

        }




    }


    public class AppsInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Appointment { get; set; }
        public long Rating { get; set; }
        
        public long NumberOfUser { get; set; }
        public long DeveloperId { get; set; }

        
    }
}

