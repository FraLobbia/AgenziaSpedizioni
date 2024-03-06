using AgenziaSpedizioni.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AgenziaSpedizioni.Controllers
{
    public class SpedizioneController : Controller
    {
        //                _____   _______   _____    ____    _   _    _____ 
        //       /\      / ____| |__   __| |_   _|  / __ \  | \ | |  / ____|
        //      /  \    | |         | |      | |   | |  | | |  \| | | (___  
        //     / /\ \   | |         | |      | |   | |  | | | . ` |  \___ \ 
        //    / ____ \  | |____     | |     _| |_  | |__| | | |\  |  ____) |
        //   /_/    \_\  \_____|    |_|    |_____|  \____/  |_| \_| |_____/ 

        // GET: Spedizione
        public ActionResult Index()
        {
            // ottieni lista spedizioni
            List<Spedizione> spedizioni = Utility.GetListaSpedizioni();
            return View(spedizioni);
        }

        // GET: Spedizione/Status/5
        public ActionResult Status(int id)
        {
            Spedizione spedizione = Utility.GetSpedizioneById(id);
            TempData["Spedizione"] = spedizione;

            List<Aggiornamenti> aggiornamenti = Utility.GetListaAggiornamenti(id);
            return View(aggiornamenti);
        }

        // GET: Spedizione/Create
        public ActionResult Create()
        {
            // ottieni lista clienti
            List<Cliente> clienti = Utility.GetListaClienti();
            ViewBag.Clienti = clienti;
            return View();
        }

        // POST: Spedizione/Create
        [HttpPost]
        public ActionResult Create(Spedizione formSpedizione)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    // crea comando
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Spedizioni (" +

                        "ClienteId, " +
                        "NumeroIdentificativo, " +
                        "DataSpedizione, " +
                        "Peso, " +
                        "CittaDestinataria, " +
                        "IndirizzoDestinatario, " +
                        "NominativoDestinatario, " +
                        "Costo, " +
                        "DataConsegnaPrevista) " +

                        "VALUES (" +

                        "@ClienteId, " +
                        "@NumeroIdentificativo, " +
                        "@DataSpedizione, " +
                        "@Peso, " +
                        "@CittaDestinataria, " +
                        "@IndirizzoDestinatario, " +
                        "@NominativoDestinatario, " +
                        "@Costo, " +
                        "@DataConsegnaPrevista)", conn);

                    System.Diagnostics.Debug.WriteLine(Request);

                    // aggiungi parametri
                    cmd.Parameters.AddWithValue("@ClienteId", formSpedizione.ClienteId);
                    // genera un numero identificativo univoco
                    string uniqueIdentificationNumber = Guid.NewGuid().ToString();
                    cmd.Parameters.AddWithValue("@NumeroIdentificativo", uniqueIdentificationNumber);
                    cmd.Parameters.AddWithValue("@DataSpedizione", formSpedizione.DataSpedizione);
                    cmd.Parameters.AddWithValue("@Peso", formSpedizione.Peso);
                    cmd.Parameters.AddWithValue("@CittaDestinataria", formSpedizione.CittaDestinataria);
                    cmd.Parameters.AddWithValue("@IndirizzoDestinatario", formSpedizione.IndirizzoDestinatario);
                    cmd.Parameters.AddWithValue("@NominativoDestinatario", formSpedizione.NominativoDestinatario);
                    cmd.Parameters.AddWithValue("@Costo", formSpedizione.Costo);
                    cmd.Parameters.AddWithValue("@DataConsegnaPrevista", formSpedizione.DataConsegnaPrevista);



                    // esegui comando
                    cmd.ExecuteNonQuery();
                    // imposta messaggio di successo
                    TempData["msgSuccess"] = "Spedizione inserita con successo!";
                }
                catch (Exception ex)
                {
                    // imposta messaggio di errore
                    TempData["msgErrore"] = "Errore: " + ex.Message;
                    ViewBag.Clienti = Utility.GetListaClienti();
                    return View();
                }
            }
            // reindirizza alla lista spedizioni
            return RedirectToAction("Index");
        }

        // GET: Spedizione/Edit/5
        public ActionResult Edit(int id)
        {
            Spedizione spedizione = Utility.GetSpedizioneById(id);
            if (spedizione.Messaggio != null)
            {
                TempData["msgErrore"] = spedizione.Messaggio;
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Spedizione/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    // crea comando
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Spedizioni " +
                        "SET " +
                        "ClienteId = @ClienteId, " +
                        "NumeroIdentificativo = @NumeroIdentificativo, " +
                        "DataSpedizione = @DataSpedizione, " +
                        "Peso = @Peso, " +
                        "CittaDestinataria = @CittaDestinataria, " +
                        "IndirizzoDestinatario = @IndirizzoDestinatario, " +
                        "NominativoDestinatario = @NominativoDestinatario, " +
                        "Costo = @Costo, " +
                        "DataConsegnaPrevista = @DataConsegnaPrevista " +
                        "WHERE Id = @Id", conn);

                    // aggiungi parametri
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@ClienteId", collection["ClienteId"]);
                    cmd.Parameters.AddWithValue("@NumeroIdentificativo", collection["NumeroIdentificativo"]);
                    cmd.Parameters.AddWithValue("@DataSpedizione", collection["DataSpedizione"]);
                    cmd.Parameters.AddWithValue("@Peso", collection["Peso"]);
                    cmd.Parameters.AddWithValue("@CittaDestinataria", collection["CittaDestinataria"]);
                    cmd.Parameters.AddWithValue("@IndirizzoDestinatario", collection["IndirizzoDestinatario"]);
                    cmd.Parameters.AddWithValue("@NominativoDestinatario", collection["NominativoDestinatario"]);
                    cmd.Parameters.AddWithValue("@Costo", collection["Costo"]);
                    cmd.Parameters.AddWithValue("@DataConsegnaPrevista", collection["DataConsegnaPrevista"]);
                    // esegui comando
                    cmd.ExecuteNonQuery();
                    // imposta messaggio di successo
                    TempData["msgSuccess"] = "Spedizione modificata con successo!";
                }
                catch (Exception ex)
                {
                    // imposta messaggio di errore
                    TempData["msgErrore"] = "Errore: " + ex.Message;
                    return View(collection);
                }
            }
            // reindirizza alla lista spedizioni
            return RedirectToAction("Index");

        }

        // GET: Spedizione/Delete/5
        public ActionResult Delete(int id)
        {
            Spedizione spedizione = Utility.GetSpedizioneById(id);
            if (spedizione.Messaggio != null)
            {
                TempData["msgErrore"] = spedizione.Messaggio;
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Spedizione/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    // crea comando
                    SqlCommand cmd = new SqlCommand("DELETE FROM Spedizioni WHERE Id = @Id", conn);
                    // aggiungi parametri
                    cmd.Parameters.AddWithValue("@Id", id);
                    // esegui comando
                    cmd.ExecuteNonQuery();
                    // imposta messaggio di successo
                    TempData["msgSuccess"] = "Spedizione eliminata con successo!";
                }
                catch (Exception ex)
                {
                    // imposta messaggio di errore
                    TempData["msgErrore"] = "Errore: " + ex.Message;
                    return View(collection);
                }
            }
            // reindirizza alla lista spedizioni
            return RedirectToAction("Index");
        }

        //    __  __   ______   _______    ____    _____    _____ 
        //   |  \/  | |  ____| |__   __|  / __ \  |  __ \  |_   _|
        //   | \  / | | |__       | |    | |  | | | |  | |   | |  
        //   | |\/| | |  __|      | |    | |  | | | |  | |   | |  
        //   | |  | | | |____     | |    | |__| | | |__| |  _| |_ 
        //   |_|  |_| |______|    |_|     \____/  |_____/  |_____|

    }
}
