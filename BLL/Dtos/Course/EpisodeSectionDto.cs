using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.Course
{
    public class EpisodeSectionDto
    {
        public int SectionNumber { get; set; }
        public string SectionTitle { get; set; }
        public int EPNum { get; set; }
        public string FilePath { get; set; }
        public int Visit { get; set; }
        public int Time { get; set; }
    }
}
