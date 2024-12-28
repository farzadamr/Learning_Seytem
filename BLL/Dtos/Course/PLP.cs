using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.Course
{
    public class PLP
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Time { get; set; }
        public string Title { get; set; }
        public int Rate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string ThumbnailPath { get; set; }
    }
}
