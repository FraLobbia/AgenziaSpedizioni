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

        public ActionResult InConsegnaOggi()
        {
            // ottieni lista spedizioni in consegna oggi
            List<Spedizione> spedizioni = Utility.GetSpedizioniInConsegnaOggi();
            return View(spedizioni);
        }

        public ActionResult InConsegnaPerCitta()
        {
            // todo: implementare
            return View();
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
            int idCreato = 0;
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

                    try
                    {
                        // crea comando
                        SqlCommand cmdFirstShipmentUpdate = new SqlCommand(
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
                        cmdFirstShipmentUpdate.Parameters.AddWithValue("@Stato", "in transito");
                        cmdFirstShipmentUpdate.Parameters.AddWithValue("@LuogoPacco", "Vercelli");
                        cmdFirstShipmentUpdate.Parameters.AddWithValue("@Descrizione", "Il pacco è stato appena spedito!");
                        cmdFirstShipmentUpdate.Parameters.AddWithValue("@DataOraAggiornamento", DateTime.Now);
                        // ottieni l'id della spedizione appena inserita
                        idCreato = Utility.GetIdSpedizioneByNumeroIdentificativo(uniqueIdentificationNumber);
                        cmdFirstShipmentUpdate.Parameters.AddWithValue("@SpedizioneId", idCreato);
                        // esegui comando
                        cmdFirstShipmentUpdate.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // se c'è un errore, mostra il messaggio di errore
                        TempData["msgErrore"] = "Errore: " + ex.Message;
                    }
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
            // reindirizza a status spedizione
            return RedirectToAction("Status", "Spedizione", new { id = idCreato });

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
            return View(spedizione);
        }

        // POST: Spedizione/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Spedizione formSpedizione)
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();

                    // doppia query NECESSARIA per i vincoli di Foreign Key, prima cancello tutti i
                    // record in AggiornamentiSpedizione con SpedizioneId = @Id,
                    // poi cancello la spedizione con Id = @Id
                    string query1 = "DELETE FROM AggiornamentiSpedizione WHERE SpedizioneId = @Id";
                    string query2 = "DELETE FROM Spedizioni WHERE Id = @Id";

                    // ciclo le due query eseguendole in sequenza
                    foreach (string query in new string[] { query1, query2 })
                    {
                        // crea comando
                        SqlCommand cmd = new SqlCommand(query, conn);
                        // aggiungi parametri
                        cmd.Parameters.AddWithValue("@Id", id);
                        // esegui comando
                        cmd.ExecuteNonQuery();
                    }

                    TempData["msgSuccess"] = "Spedizione eliminata con successo!";
                }
                catch (Exception ex)
                {
                    // imposta messaggio di errore
                    TempData["msgErrore"] = "Errore: " + ex.Message;
                    return View(formSpedizione);
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
