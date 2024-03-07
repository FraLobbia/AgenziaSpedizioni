using AgenziaSpedizioni.Models;
using System;
using System.Data.SqlClient;
using System.Web.Mvc;
namespace AgenziaSpedizioni.Controllers
{
    [Authorize]
    public class AggiornamentiController : Controller
    {
        // GET: Aggiornamenti

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Spedizione");
        }


        // GET: Aggiornamenti/Create
        public ActionResult Create(int id)
        {
            // ottieni la spedizione tramite l'id
            Spedizione spedizione = Utility.GetSpedizioneById(id);
            TempData["Spedizione"] = spedizione;
            return View();
        }

        // POST: Aggiornamenti/Create

        [HttpPost]
        public ActionResult Create(int id, Aggiornamenti formAggiornamenti)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    // crea comando
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO AggiornamentiSpedizione (" +
                        "Stato, " +
                        "LuogoPacco, " +
                        "Descrizione, " +
                        "DataAggiornamento, " +
                        "SpedizioneId) " +
                        "VALUES (" +
                        "@Stato, " +
                        "@LuogoPacco, " +
                        "@Descrizione, " +
                        "@DataOraAggiornamento, " +
                        "@SpedizioneId)", conn);
                    // aggiungi parametri
                    cmd.Parameters.AddWithValue("@Stato", formAggiornamenti.Stato);
                    cmd.Parameters.AddWithValue("@LuogoPacco", formAggiornamenti.LuogoPacco);
                    cmd.Parameters.AddWithValue("@Descrizione", formAggiornamenti.Descrizione);
                    cmd.Parameters.AddWithValue("@DataOraAggiornamento", formAggiornamenti.DataOraAggiornamento);
                    cmd.Parameters.AddWithValue("@SpedizioneId", id);
                    // esegui comando
                    cmd.ExecuteNonQuery();
                    // chiudi connessione
                    conn.Close();
                    // reindirizza alla pagina Status del controller Spedizione
                    return RedirectToAction("Status", "Spedizione", new { id = id });
                }
                catch (Exception ex)
                {
                    // se c'è un errore, mostra il messaggio di errore
                    TempData["msgErrore"] = "Errore: " + ex.Message;
                    return RedirectToAction("Status", "Spedizione", new { id = id });
                }
            }
        }

        // GET: Aggiornamenti/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Aggiornamenti/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Aggiornamenti/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Aggiornamenti/Delete/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
