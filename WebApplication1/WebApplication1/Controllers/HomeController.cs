using System;
using System.Data;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
		private const string dbConnectionString = "server=209.106.201.103;uid=dbstudent24;pwd=greatdugong72;database=group4";

		[HttpGet]
		public IActionResult Index()
		{
			MySqlConnection conn;
			MySqlCommand cmd;
			MySqlDataReader reader;

			conn = new MySqlConnection();
			conn.ConnectionString = dbConnectionString;

			try
			{
				cmd = new MySqlCommand();
				cmd.Connection = conn;
				cmd.CommandType = CommandType.Text;
				conn.Open();
				cmd.CommandText = String.Format("select first_name, Contact.contactInfo from Student natural join Contact where Contact.type = \"Email\";");

				reader = cmd.ExecuteReader();
			}
			catch (MySql.Data.MySqlClient.MySqlException ex)
			{
				ViewData["Message"] = ex.Message;
			}

			conn.CloseAsync();

			return View();
		}

    public IActionResult EditStudent(List<string> student)
    {
      return View(student);
    }

	[HttpPost]
	public IActionResult DeleteStudent(List<string> student)
		{
			MySqlConnection conn;
			MySqlCommand cmd;
			MySqlDataReader reader;

			conn = new MySqlConnection();
			conn.ConnectionString = dbConnectionString;

			try
			{
				cmd = new MySqlCommand();
				cmd.Connection = conn;
				cmd.CommandType = CommandType.Text;
				conn.Open();
				cmd.CommandText = String.Format("DELETE FROM Student WHERE studentID = {0}", student[0]);
				ViewData["Message"] = "Data deleted";

				reader = cmd.ExecuteReader();
			}
			catch (MySql.Data.MySqlClient.MySqlException ex)
			{
				ViewData["Message"] = ex.Message;
			}

			conn.CloseAsync();

			return View(student);
		}


	public IActionResult EmailStudents()
    {
      // For help with the connection see:
      // https://dev.mysql.com/doc/connector-net/en/connector-net-programming-connecting-connection-string.html
      MySqlConnection conn;
      MySqlCommand cmd;

      var model = new List<List<String>>();
      try
      {
        conn = new MySqlConnection();
        conn.ConnectionString = dbConnectionString;
        cmd = new MySqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;
        conn.Open();

        cmd.CommandText = String.Format("Select * from Student Natural Join Contact where Contact.Type = \"Email\" Order By Major");


        MySqlDataReader sqlReader = cmd.ExecuteReader();
        while (sqlReader.Read())
        {
          List<string> SqlList = new List<string>();

          SqlList.Add(sqlReader["ContactInfo"].ToString());

          model.Add(SqlList);
        }

        conn.CloseAsync();
      }
      catch (MySql.Data.MySqlClient.MySqlException ex)
      {
        ViewData["Message"] = ex.Message;
      }

      return View(model);
    }

    public IActionResult ShowAllTables()
    {
      // For help with the connection see:
      // https://dev.mysql.com/doc/connector-net/en/connector-net-programming-connecting-connection-string.html
      MySqlConnection conn;
      MySqlCommand cmd;

      var model = new List<List<String>>();
      try
      {
        conn = new MySqlConnection();
        conn.ConnectionString = dbConnectionString;
        cmd = new MySqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;
        conn.Open();

        cmd.CommandText = String.Format("Select * from Student Natural Join Contact where Contact.Type = \"Phone\" Order By Major");

        MySqlDataReader sqlReader = cmd.ExecuteReader();
        while (sqlReader.Read())
        {
          List<string> SqlList = new List<string>();

          SqlList.Add(sqlReader["studentID"].ToString());
          SqlList.Add(sqlReader["first_name"].ToString());
          SqlList.Add(sqlReader["last_name"].ToString());
          SqlList.Add(sqlReader["Address"].ToString());
          SqlList.Add(sqlReader["Major"].ToString());
          SqlList.Add(sqlReader["contactInfo"].ToString());

          model.Add(SqlList);
        }

        conn.CloseAsync();
      }
      catch (MySql.Data.MySqlClient.MySqlException ex)
      {
        ViewData["Message"] = ex.Message;
      }

      return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
		public IActionResult Index(IndexPageModel indexModel)
		{
			// For help with the connection see:
			// https://dev.mysql.com/doc/connector-net/en/connector-net-programming-connecting-connection-string.html
			MySqlConnection conn;
			MySqlCommand cmd;
			MySqlDataReader reader;
			conn = new MySqlConnection();
			conn.ConnectionString = dbConnectionString;

			try
			{
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

			conn.CloseAsync();

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
