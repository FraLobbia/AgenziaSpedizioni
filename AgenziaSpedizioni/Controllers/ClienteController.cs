using AgenziaSpedizioni.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AgenziaSpedizioni.Controllers
{
    public class ClienteController : Controller
    {
        //                _____   _______   _____    ____    _   _    _____ 
        //       /\      / ____| |__   __| |_   _|  / __ \  | \ | |  / ____|
        //      /  \    | |         | |      | |   | |  | | |  \| | | (___  
        //     / /\ \   | |         | |      | |   | |  | | | . ` |  \___ \ 
        //    / ____ \  | |____     | |     _| |_  | |__| | | |\  |  ____) |
        //   /_/    \_\  \_____|    |_|    |_____|  \____/  |_| \_| |_____/ 

        // GET: Cliente
        public ActionResult Index()
        {
            // ottengo la lista dei clienti
            List<Cliente> clienti = Utility.GetListaClienti();
            // se c'è una property Messaggio, vuol dire che c'è stato un errore
            if (clienti.Count > 0 && clienti[0].Messaggio != null)
            {
                ViewBag.msgErrore = clienti[0].Messaggio;
            }
            ViewBag.msgSuccess = TempData["msgSuccess"];
            return View(clienti);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            Cliente cliente = Utility.GetClienteById(id);
            if (cliente.Messaggio != null)
            {
                ViewBag.msgErrore = cliente.Messaggio;
            }
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(Cliente formCliente)
        {
            if (!ModelState.IsValid)
            {
                return View(formCliente);
            }

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = "";
                    if (formCliente.TipoCliente == "Privato")
                    {
                        query = ("" +
                          "INSERT INTO Clienti " +
                          "(Nome, TipoCliente, CodiceFiscale) " +
                          "VALUES (@Nome, @TipoCliente, @CodiceFiscale)");

                    }
                    else if (formCliente.TipoCliente == "Azienda")
                    {
                        query = ("" +
                          "INSERT INTO Clienti " +
                          "(Nome, TipoCliente, PartitaIva) " +
                          "VALUES (@Nome, @TipoCliente, @PartitaIva)");
                    }
                    // crea comando
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // aggiungi parametri
                    cmd.Parameters.AddWithValue("@Nome", formCliente.Nome);
                    cmd.Parameters.AddWithValue("@TipoCliente", formCliente.TipoCliente);
                    if (formCliente.TipoCliente == "Privato")
                    {
                        cmd.Parameters.AddWithValue("@CodiceFiscale", formCliente.CodiceFiscale);
                    }
                    else if (formCliente.TipoCliente == "Azienda")
                    {
                        cmd.Parameters.AddWithValue("@PartitaIva", formCliente.PartitaIva);
                    }

                    // esegui comando
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    ViewBag.msgErrore = "Errore: " + ex.Message;
                    return View(formCliente);
                }
                finally
                {
                    conn.Close();
                }

                TempData["msgSuccess"] = "Cliente" + formCliente.Nome + " inserito correttamente";
                return RedirectToAction("Index");
            }

        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            Cliente cliente = Utility.GetClienteById(id);
            if (cliente.Messaggio != null)
            {
                ViewBag.msgErrore = cliente.Messaggio;
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();
                    // crea comando
                    SqlCommand cmd = new SqlCommand("" +
                        "UPDATE Clienti " +
                        "SET Nome = @Nome, " +
                        "TipoCliente = @TipoCliente, " +
                        "CodiceFiscale = @CodiceFiscale, " +
                        "PartitaIva = @PartitaIva " +
                        "WHERE Id = @Id", conn);

                    // aggiungi parametri
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Nome", collection["Nome"]);
                    cmd.Parameters.AddWithValue("@TipoCliente", collection["TipoCliente"]);
                    cmd.Parameters.AddWithValue("@CodiceFiscale", collection["CodiceFiscale"]);
                    cmd.Parameters.AddWithValue("@PartitaIva", collection["PartitaIva"]);

                    // esegui comando
                    cmd.ExecuteNonQuery();
                    TempData["msgSuccess"] = "Cliente " + collection["Nome"] + " modificato correttamente";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.msgErrore = "Errore: " + ex.Message;
                    return View();
                }
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            Cliente cliente = Utility.GetClienteById(id);
            if (cliente.Messaggio != null)
            {
                ViewBag.msgErrore = cliente.Messaggio;
            }
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();
                    // crea comando
                    SqlCommand cmd = new SqlCommand("DELETE FROM Clienti WHERE Id = @Id", conn);
                    // aggiungi parametri
                    cmd.Parameters.AddWithValue("@Id", id);
                    // esegui comando
                    cmd.ExecuteNonQuery();
                    TempData["msgSuccess"] = "Cliente eliminato correttamente";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.msgErrore = "Errore: " + ex.Message;
                    return View();
                }
        }

        //    __  __   ______   _______    ____    _____    _____ 
        //   |  \/  | |  ____| |__   __|  / __ \  |  __ \  |_   _|
        //   | \  / | | |__       | |    | |  | | | |  | |   | |  
        //   | |\/| | |  __|      | |    | |  | | | |  | |   | |  
        //   | |  | | | |____     | |    | |__| | | |__| |  _| |_ 
        //   |_|  |_| |______|    |_|     \____/  |_____/  |_____|

        // Metodo per verificare se il nome del cliente è disponibile
        // Richiede il parametro Nome in formato stringa
        // Restituisce un valore booleano in formato JSON 
        public ActionResult IsNomeClienteAvailable()
        {
            string nome = Request["Nome"];
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Clienti WHERE Nome = @Nome";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nome);
                        int count = (int)cmd.ExecuteScalar(); // Restituisce solo la prima colonna della prima riga, ignorando le altre colonne e righe
                        if (count > 0)
                        {
                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (SqlException e)
                {
                    return Json("Errore di connessione al database: " + e.Message, JsonRequestBehavior.AllowGet);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        // Metodo per verifica se il codice fiscale è disponibile
        // Richiede il parametro CodiceFiscale in formato stringa
        // Restituisce un valore booleano in formato JSON
        public ActionResult IsCodiceFiscaleAvailable()
        {
            string codiceFiscale = Request["CodiceFiscale"];
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Clienti WHERE CodiceFiscale = @CodiceFiscale";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CodiceFiscale", codiceFiscale);
                        int count = (int)cmd.ExecuteScalar(); // Restituisce solo la prima colonna della prima riga, ignorando le altre colonne e righe
                        if (count > 0)
                        {
                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (SqlException e)
                {
                    return Json("Errore di connessione al database: " + e.Message, JsonRequestBehavior.AllowGet);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        // Metodo per verifica se la partita iva è disponibile
        // Richiede il parametro PartitaIva in formato stringa
        // Restituisce un valore booleano in formato JSON
        public ActionResult IsPartitaIvaAvailable()
        {
            string partitaIva = Request["PartitaIva"];
            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Clienti WHERE PartitaIva = @PartitaIva";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PartitaIva", partitaIva);
                        int count = (int)cmd.ExecuteScalar(); // Restituisce solo la prima colonna della prima riga, ignorando le altre colonne e righe
                        if (count > 0)
                        {
                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (SqlException e)
                {
                    return Json("Errore di connessione al database: " + e.Message, JsonRequestBehavior.AllowGet);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
