using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Courses
{
	public class Course
	{
		public long Id { get; set; }
		public long TeacherId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime UpdateTime { get; set; }
		public int Capacity { get; set; }
		public string TumbnailPath { get; set; }
		public string DemoVideoPath { get; set; }
		public TimeOnly Time { get; set; }
		public int Rate { get; set; }
		public CourseStatus Status { get; set; }
	}
	public enum CourseStatus
	{
		completed = 10,
		Registering = 20,
	}
}
