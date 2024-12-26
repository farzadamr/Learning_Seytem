using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.Course
{
    public class AddCourseDto
    {
        public int TeacherId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public IFormFile Tumbnail { get; set; }
        public IFormFile? DemoVideo { get; set; }
        public int Time { get; set; }
        public string Status { get; set; }
    }
}
