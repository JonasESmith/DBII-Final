﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
	public class IndexPageModel
	{
		public class StudentModel
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string Address { get; set; }
			public string GradDate { get; set; }
		}


		public StudentModel Student { get; set; }
		public List<Object> Contacts { get; set; }
	}
}