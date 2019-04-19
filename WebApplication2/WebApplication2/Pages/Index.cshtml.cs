using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;

namespace WebApplication2.Pages
{
    public class IndexModel : PageModel
    {
			// For help with the connection see:
			// https://dev.mysql.com/doc/connector-net/en/connector-net-programming-connecting-connection-string.html
			MySqlConnection conn;

		public IndexModel()
		{
			try
			{
				conn = new MySqlConnection();
				conn.ConnectionString = "server=209.106.201.103;uid=dbstudent24;pwd=greatdugong72;database=group4";
				
			}
			catch (MySql.Data.MySqlClient.MySqlException ex)
			{
				//MessageBox.Show(ex.Message);
			}
		}

        public void OnGet()
        {

        }

		public void OnPost()
		{
			var firstName = Request.Form["firstName"];
			var lastName = Request.Form["lastName"];
			var address = Request.Form["address"];
			var gradDate = Request.Form["gradDate"];
			conn.Open();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = String.Format("insert into TestTable values (NULL, \"{0}\",\"{1}\",\"{2}\",\"{3}\");", firstName, lastName, address, gradDate);
			cmd.Connection = conn;
			cmd.CommandType = CommandType.Text;
			MySqlDataReader reader = cmd.ExecuteReader();
		}
	}

	public class ProductContext : DbContext
	{
		public DbSet<Object> Categories { get; set; }
		public DbSet<Object> Products { get; set; }
	}
}
