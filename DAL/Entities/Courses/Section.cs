using DAL.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Courses
{
	public class Section
	{
		public long Id { get; set; }
        public long CourseId { get; set; }
        public string Title { get; set; }
    }
}
