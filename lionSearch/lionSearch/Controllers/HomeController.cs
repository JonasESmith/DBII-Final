using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace lionSearch.Controllers
{
  public class HomeController : Controller
  {
    private const string dbConnectionString = "server=209.106.201.103;uid=dbstudent24;pwd=greatdugong72;database=group4";

    public ActionResult Index()
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
        cmd.CommandText = string.Format("select first_name, Contact.contactInfo from Student natural join Contact where Contact.type = \"Email\";");

        reader = cmd.ExecuteReader();
      }
      catch (MySql.Data.MySqlClient.MySqlException ex)
      {
        ViewData["Message"] = ex.Message;
      }

      conn.CloseAsync();

      return View();
    }

    public ActionResult EditStudent(List<string> student)
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
        cmd.CommandText = string.Format("UPDATE Student SET first_name = \"{0}\", last_name = \"{1}\", Address = \"{2}\", Major = \"{3}\" " +
                        "WHERE studentID = {4};",
                        student[1], student[2], student[3], student[4], student[0]);
        ViewData["Message"] = cmd.CommandText;
        reader = cmd.ExecuteReader();

        cmd.CommandText = string.Format("UPDATE Contact SET contactInfo = \"{0}\" WHERE studentID = {1}",
                        student[5], student[0]);
      }
      catch (MySql.Data.MySqlClient.MySqlException ex)
      {
        ViewData["Message"] = ex.Message;
      }

      conn.CloseAsync();

      return View(student);
    }

    [HttpPost]
    public ActionResult DeleteStudent(List<string> student)
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
        cmd.CommandText = string.Format("DELETE FROM Student WHERE studentID = {0}", student[0]);
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

    public ActionResult ShowAllTables()
    {
      // For help with the connection see:
      // https://dev.mysql.com/doc/connector-net/en/connector-net-programming-connecting-connection-string.html
      MySqlConnection conn;
      MySqlCommand cmd;

      var model = new List<List<string>>();
      try
      {
        conn = new MySqlConnection();
        conn.ConnectionString = dbConnectionString;
        cmd = new MySqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;
        conn.Open();

        cmd.CommandText = string.Format("Select * from Student Natural Join Contact where Contact.Type = \"Phone\" Order By Major");

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

    public ActionResult StudentEmail()
    {
      // For help with the connection see:
      // https://dev.mysql.com/doc/connector-net/en/connector-net-programming-connecting-connection-string.html
      MySqlConnection conn;
      MySqlCommand cmd;

      var model = new List<List<string>>();
      try
      {
        conn = new MySqlConnection();
        conn.ConnectionString = dbConnectionString;
        cmd = new MySqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;
        conn.Open();

        cmd.CommandText = string.Format("Select * from Student Natural Join Contact where Contact.Type = \"Email\" Order By Major");


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

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}