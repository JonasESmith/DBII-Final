using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
	public class IndexPageModel
	{
		public class StudentModel
		{
			public int StudentID { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string Address { get; set; }
			public string Major { get; set; }
			public string GradDate { get; set; }
		}

		public class ContactModel
		{
			public int StudentID { get; set; }
			public int ContactID { get; set; }
			public string Type { get; set; }
			public string ContactInfo { get; set; }
		}

		public StudentModel Student { get; set; }
		public List<ContactModel> Contacts { get; set; }
	}
}
