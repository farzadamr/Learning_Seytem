using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.Users
{
    public class StudentDto
    {
        public int Id { get; set; }
        public bool? Lockout { get; set; }
        public string Major { get; set; }
        public string? LinkedId { get; set; }
        public string? ActivityArea { get; set; }
        public int PersonId { get; set; }
    }
}
