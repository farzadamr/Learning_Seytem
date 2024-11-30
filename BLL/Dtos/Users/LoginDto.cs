
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.Users
{
    public class LoginDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public ClaimDto claims { get; set; }
    }
    public class ClaimDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
