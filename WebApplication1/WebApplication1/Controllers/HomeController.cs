﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Index(IndexPageModel indexModel)
		{
			// For help with the connection see:
			// https://dev.mysql.com/doc/connector-net/en/connector-net-programming-connecting-connection-string.html
			MySqlConnection conn;
			MySqlCommand cmd;
			MySqlDataReader reader;

			try
			{
				conn = new MySqlConnection();
				conn.ConnectionString = "server=209.106.201.103;uid=dbstudent24;pwd=greatdugong72;database=group4";
				cmd = new MySqlCommand();
				cmd.Connection = conn;
				cmd.CommandType = CommandType.Text;
				conn.Open();
				cmd.CommandText = String.Format("INSERT INTO Student VALUES (NULL, \"{0}\", \"{1}\", \"{2}\", \"{3}\", NULL);",
												indexModel.Student.FirstName, indexModel.Student.LastName, indexModel.Student.Address,
												indexModel.Student.Major, indexModel.Student.GradDate);

				reader = cmd.ExecuteReader();

				cmd.CommandText = String.Format("INSERT INTO Contact VALUES ((SELECT studentID FROM Student WHERE first_name = \"{0}\" AND "
												+ "last_name = \"{1}\" AND Address = \"{2}\" AND Major = \"{3}\" AND Expected_Graduation IS NULL) "
												+ ", NULL, \"{5}\", \"{6}\")",
												indexModel.Student.FirstName, indexModel.Student.LastName, indexModel.Student.Address,
												indexModel.Student.Major, indexModel.Student.GradDate, indexModel.Contact.Type, indexModel.Contact.ContactInfo);

				reader = cmd.ExecuteReader();
				ViewData["Message"] = "Data successfully submitted.";
			}
			catch (MySql.Data.MySqlClient.MySqlException ex)
			{
				ViewData["Message"] = ex.Message;
			}

			return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
