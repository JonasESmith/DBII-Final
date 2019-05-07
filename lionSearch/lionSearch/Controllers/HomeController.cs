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
      return View();
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