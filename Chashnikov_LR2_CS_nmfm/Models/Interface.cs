using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chashnikov_LR2_CS.Models
{
    public interface InterfaceofDb
    {


        List<AppsInfo> ShowAllApps(List<Application> apps);



        List<AppsInfo> UsersApp(List<UsersApps> usersapps, long id, List<Application> apps);

        // Вывод приложений по количеству пользователей
        List<AppsInfo> AppsbyNOF(List<Application> apps, bool sort);
       
        List<AppsInfo> Top5AppsbyNOF(List<Application> apps, string appointment, bool sort);



        //Отработка по рейтингу
        List<AppsInfo> AppsbyRating(List<Application> apps, bool sort);
        List<AppsInfo> Top5AppsbyRating(List<Application> apps, string appointment, bool sort);






        List<AppsInfo> SelectbyAppointment(List<Application> apps, string appointment);

        List<AppsInfo> DevelopersApplications(List<Application> apps, long id);
        


    

         List<string> ShowAllCompany(List<Developer> developers);



        IEnumerable<IEnumerable<string>> SelectAppsbyCompany( List<Developer> devs, string company);

        List<string> SelectCompany(List<Developer> devs, string company);



    }
}
