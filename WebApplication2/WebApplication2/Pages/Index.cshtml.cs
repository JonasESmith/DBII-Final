using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

		public void OnPost()
		{
			var firstName = Request.Form["firstName"];
			var lastName = Request.Form["lastName"];
			var address = Request.Form["address"];
			var gradDate = Request.Form["gradDate"];
		}
	}

	public class ProductContext : DbContext
	{
		public DbSet<Object> Categories { get; set; }
		public DbSet<Object> Products { get; set; }
	}
}
