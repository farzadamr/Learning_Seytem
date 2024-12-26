using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.Course
{
    public class CourseListDto
    {
        public int Id { get; set; }
        public string TeacherName { get; set; }
        public string CategoryType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Capacity { get; set; }
        public string ThumbnailPath { get; set; }
        public string? DemoVideoPath { get; set; }
        public int Time { get; set; }
        public int Rate { get; set; }
        public string Status { get; set; }
        public int Price { get; set; }

    }
}
