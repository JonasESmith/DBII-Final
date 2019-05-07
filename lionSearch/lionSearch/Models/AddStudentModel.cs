using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lionSearch.Models
{
	public class AddStudentModel
	{
		public StudentModel Student { get; set; }
		public ContactModel Contact { get; set; }

		public AddStudentModel()
		{
			Student = new StudentModel();
			Contact = new ContactModel();
		}
	}
}
