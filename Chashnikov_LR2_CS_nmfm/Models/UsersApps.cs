using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chashnikov_LR2_CS.Models
{
    public class UsersApps
    {
        public long Id { get; set; }
       public long UserId { get; set; }
        public long ApplicationId { get; set; }
        public User User { get; set; }



    }
}
