using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chashnikov_LR2_CS.Models
{
    public class BLogic : InterfaceofDb
    {


        public List<AppsInfo> ShowAllApps(List<Application> apps)

        {
            return apps.Select(u => new AppsInfo { Id = u.Id, Name = u.Name, Appointment = u.Appointment, Rating = u.Rating, NumberOfUser = u.NumberOfUser, DeveloperId = u.DeveloperId }).ToList();

        }

  




        public List<AppsInfo> UsersApp(List<UsersApps> usersapps, long id, List<Application> apps)
        {

            var apid = usersapps.Where(u => u.UserId == id).Select(t => t.ApplicationId).ToList();
            return (from long Idapp in apid
                    select apps.FirstOrDefault(a => a.Id == Idapp).GetAppsInfo()).ToList();
        }


        public List<AppsInfo> SelectbyAppointment(List<Application> apps, string appointment)
        {
             return ShowAllApps(apps.Where(u => u.Appointment == appointment).ToList());
        }


        public List<AppsInfo> DevelopersApplications(List<Application> apps, long id)
        {
            return ShowAllApps(apps.Where(u => u.DeveloperId == id).ToList());
        }




        public List<string> ShowAllCompany(List<Developer> developers)
        {
            return developers.Select(u => u.Company).Distinct().ToList();
        }



        //NOF -Number Of Users
        // sort 1 means sorting in descending order, 2 means sorting in ascending order 
        public List<AppsInfo> AppsbyNOF(List<Application> apps, bool sort)
        {
            if (sort == true)
                return ShowAllApps(apps).OrderByDescending(a => a.NumberOfUser).ToList();
            else return ShowAllApps(apps).OrderBy(a => a.NumberOfUser).ToList();
        }



        //NOF -Number Of Users
        // sort true means sorting in descending order, false means sorting in ascending order 
        public List<AppsInfo> Top5AppsbyNOF(List<Application> apps, string appointment, bool sort)
        {
            if (sort == true)
            return ShowAllApps(apps).Where(a=>a.Appointment==appointment).OrderByDescending(a => a.NumberOfUser).Take(5).ToList();
       else return ShowAllApps(apps).Where(a => a.Appointment == appointment).OrderBy(a => a.NumberOfUser).Take(5).ToList();

        }


        public List<AppsInfo> AppsbyRating(List<Application> apps, bool sort)
        { 
        if (sort==true)
                return ShowAllApps(apps).OrderByDescending(a => a.Rating).ToList();
            else return ShowAllApps(apps).OrderBy(a => a.Rating).ToList();


        }



        public List<AppsInfo> Top5AppsbyRating(List<Application> apps, string appointment, bool sort)
        {
            if (sort == true)
                return ShowAllApps(apps).Where(a => a.Appointment == appointment).OrderByDescending(a => a.Rating).Take(5).ToList();
            else return ShowAllApps(apps).Where(a => a.Appointment == appointment).OrderBy(a => a.Rating).Take(5).ToList();

        }






        public IEnumerable<IEnumerable<string>> SelectAppsbyCompany(List <Developer> devs, string company)
        {
            var  selected_apps = devs.Where(d => d.Company == company).Select(u => u.Applications.Select(a=>a.Name));

            return selected_apps;
        }

        public List<string> SelectCompany(List<Developer> devs, string company)
        {
            List<string> apps = new List<string>();


           foreach (var dev in devs.Where(dev => dev.Company == company))
            {
                apps.AddRange(dev.Applications.Select(a => a.Name).ToList());
            }
            return apps;

           
        }

       
    }
}
