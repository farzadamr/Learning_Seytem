using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Courses
{
	public class Episode
	{
		public long Number { get; set; }
        public long CourseId { get; set; }
		public long SectionId { get; set; }
        public TimeOnly Time { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public int Visit { get; set; }
	}
}
