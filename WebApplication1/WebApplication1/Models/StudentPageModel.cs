﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
	public class StudentPageModel
	{
		public Tuple<StudentModel, ContactModel> StudentInfo { get; set; }
	}
}
