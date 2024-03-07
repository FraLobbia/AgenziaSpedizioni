using AgenziaSpedizioni.Models;
using System;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;

namespace AgenziaSpedizioni.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Utente u)
        {
            string role = "";
            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @Username AND Password = @Password", conn);
                    cmd.Parameters.AddWithValue("Username", u.Username);
                    cmd.Parameters.AddWithValue("Password", u.Password);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        FormsAuthentication.SetAuthCookie(u.Username, false);
                        while (reader.Read())
                        {
                            role = reader.GetString(3);
                        }
                        TempData["msgSuccess"] = "Login come " + role + " effettuato con successo";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        conn.Close();
                        ViewBag.AuthError = "Autenticazione non riuscita";
                        return View();

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.AuthError = ex.Message;
                }
                finally { conn.Close(); }

            return RedirectToAction("Index", "Home");


        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["msgSuccess"] = "Logout effettuato con successo";
            return RedirectToAction("Index", "Home");
        }
    }
}