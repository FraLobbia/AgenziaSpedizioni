using System;
using System.Data.SqlClient;
using System.Web.Mvc;
namespace AgenziaSpedizioni.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult CheckStatus()
        {
            // ottieni connessione
            // SqlConnection conn = Connection.GetConn();
            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();
                // crea comando
                SqlCommand cmd = new SqlCommand("SELECT * FROM Spedizione", conn);
                // esegui comando
                SqlDataReader reader = cmd.ExecuteReader();
                // leggi risultati
                while (reader.Read())
                {
                    Console.WriteLine(reader["Id"]);
                }
            }
            return View();
        }
    }
}