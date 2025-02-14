﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Courses
{
	public class Course
	{
		public int Id { get; set; }
		public int TeacherId { get; set; }
		public int CategoryId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime UpdateTime { get; set; }
		public int Capacity { get; set; }
		public string TumbnailPath { get; set; }
		public string DemoVideoPath { get; set; }
		public int Time { get; set; }
		public int Rate { get; set; }
		public string Status { get; set; }
	}
}
