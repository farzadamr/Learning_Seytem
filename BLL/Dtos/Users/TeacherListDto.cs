using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.Users
{
    public class TeacherListDto
    {
        public int Id { get; set; }
        public string AvatarPath { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string Resume { get; set; }
        public int PersonId { get; set; }
    }
}
