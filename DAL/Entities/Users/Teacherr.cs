using DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Users
{
    public class Teacherr 
    {
        public int Id { get; set; }
        public string Resume { get; set; }
        public int PersonId { get; set; }
    }
}
