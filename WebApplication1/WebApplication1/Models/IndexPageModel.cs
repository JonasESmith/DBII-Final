using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;

namespace WebApplication1.Models
{
	public class IndexPageModel
	{
		public StudentModel Student { get; set; }
		public ContactModel Contact { get; set; }
	}
}
