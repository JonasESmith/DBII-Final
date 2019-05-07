using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{

	public class ContactModel
	{
		public enum ContactType { Email, Phone }
		public int StudentID { get; set; }
		public int ContactID { get; set; }
		public ContactType Type { get; set; }
		public string ContactInfo { get; set; }
	}
}
