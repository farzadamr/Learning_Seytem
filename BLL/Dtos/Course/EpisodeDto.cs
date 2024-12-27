using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.Course
{
    public class EpisodeDto
    {
        public int Id { get; set; }
        public int number { get; set; }
        public int sectionId { get; set; }
        public int CourseId { get; set; }
        public int Time { get; set; }
        public int Visit { get; set; }
        public string FilePath { get; set; }
    }
}
