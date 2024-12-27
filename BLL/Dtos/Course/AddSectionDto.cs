using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.Course
{
    public class AddSectionDto
    {
        public int number { get; set; }
        public string Title { get; set; }
        public int courseId { get; set; }
    }
}
