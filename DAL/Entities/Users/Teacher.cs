using DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Users
{
    public class Teacher : Personn
    {
        public string Resume { get; set; }
    }
}
