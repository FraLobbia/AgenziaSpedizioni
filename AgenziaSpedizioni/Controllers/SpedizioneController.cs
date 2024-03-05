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
            List<Spedizione> spedizioni = GetListaSpedizioni();
            return View(spedizioni);
        }

        // GET: Spedizione/Details/5
        public ActionResult Details(int id)
        {
            // ottieni spedizione tramite id
            Spedizione spedizione = GetSpedizioneById(id);
            return View(spedizione);
        }

        // GET: Spedizione/Create
        public ActionResult Create()
        {
            // ottieni lista clienti
            List<Cliente> clienti = GetListaClienti();
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

                        "VALUES " +

                        "(@ClienteId, " +
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
                    // imposta messaggio di successo
                    TempData["msgSuccess"] = "Spedizione inserita con successo!";
                }
                catch (Exception ex)
                {
                    // imposta messaggio di errore
                    TempData["msgErrore"] = "Errore: " + ex.Message;
                    ViewBag.Clienti = GetListaClienti();
                    return View(formSpedizione);
                }
            }
            // reindirizza alla lista spedizioni
            return RedirectToAction("Index");
        }

        // GET: Spedizione/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Spedizione/Edit/5
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

        // GET: Spedizione/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Spedizione/Delete/5
        [HttpPost]
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

        //    __  __   ______   _______    ____    _____    _____ 
        //   |  \/  | |  ____| |__   __|  / __ \  |  __ \  |_   _|
        //   | \  / | | |__       | |    | |  | | | |  | |   | |  
        //   | |\/| | |  __|      | |    | |  | | | |  | |   | |  
        //   | |  | | | |____     | |    | |__| | | |__| |  _| |_ 
        //   |_|  |_| |______|    |_|     \____/  |_____/  |_____|

        // Meotodo per ottenere la lista delle spedizioni
        // Non riceve parametri
        // Restituisce una lista di oggetti Spedizione
        public List<Spedizione> GetListaSpedizioni()
        {
            // crea lista di spedizioni
            List<Spedizione> spedizioni = new List<Spedizione>();

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    // crea comando
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Spedizioni", conn);
                    // esegui comando
                    SqlDataReader reader = cmd.ExecuteReader();
                    // leggi risultati
                    while (reader.Read())
                    {
                        // crea oggetto spedizione
                        Spedizione spedizione = new Spedizione();
                        spedizione.Id = Convert.ToInt32(reader["Id"]);
                        spedizione.ClienteId = Convert.ToInt32(reader["ClienteId"]);
                        spedizione.NumeroIdentificativo = reader["NumeroIdentificativo"].ToString();
                        spedizione.DataSpedizione = Convert.ToDateTime(reader["DataSpedizione"]);
                        spedizione.Peso = Convert.ToDecimal(reader["Peso"]);
                        spedizione.CittaDestinataria = reader["CittaDestinataria"].ToString();
                        spedizione.IndirizzoDestinatario = reader["IndirizzoDestinatario"].ToString();
                        spedizione.NominativoDestinatario = reader["NominativoDestinatario"].ToString();
                        spedizione.Costo = Convert.ToDecimal(reader["Costo"]);
                        spedizione.DataConsegnaPrevista = Convert.ToDateTime(reader["DataConsegnaPrevista"]);
                        // aggiungi spedizione alla lista
                        spedizioni.Add(spedizione);
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Errore: " + ex.Message;
                }
            }

            return spedizioni;
        }

        // Metodo per ottenere una spedizione tramite id
        // Riceve un intero id
        // Restituisce un oggetto Spedizione
        public Spedizione GetSpedizioneById(int id)
        {
            // crea oggetto spedizione
            Spedizione spedizione = new Spedizione();

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    // crea comando
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Spedizioni WHERE Id = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    // esegui comando
                    SqlDataReader reader = cmd.ExecuteReader();
                    // leggi risultati
                    while (reader.Read())
                    {
                        spedizione.Id = Convert.ToInt32(reader["Id"]);
                        spedizione.ClienteId = Convert.ToInt32(reader["ClienteId"]);
                        spedizione.NumeroIdentificativo = reader["NumeroIdentificativo"].ToString();
                        spedizione.DataSpedizione = Convert.ToDateTime(reader["DataSpedizione"]);
                        spedizione.Peso = Convert.ToDecimal(reader["Peso"]);
                        spedizione.CittaDestinataria = reader["CittaDestinataria"].ToString();
                        spedizione.IndirizzoDestinatario = reader["IndirizzoDestinatario"].ToString();
                        spedizione.NominativoDestinatario = reader["NominativoDestinatario"].ToString();
                        spedizione.Costo = Convert.ToDecimal(reader["Costo"]);
                        spedizione.DataConsegnaPrevista = Convert.ToDateTime(reader["DataConsegnaPrevista"]);
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Errore: " + ex.Message;
                }
            }

            return spedizione;
        }

        // Metodo per ottenere la lista dei clienti dal db dalla tabella Cliente
        // Non richiede parametri
        // Restituisce una lista di oggetti di tipo Cliente
        public List<Cliente> GetListaClienti()
        {
            // crea lista di clienti
            List<Cliente> clienti = new List<Cliente>();

            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();
                    // crea comando
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Clienti", conn);
                    // esegui comando
                    SqlDataReader reader = cmd.ExecuteReader();
                    // leggi risultati
                    while (reader.Read())
                    {
                        // crea oggetto cliente
                        Cliente cliente = new Cliente();
                        cliente.Id = Convert.ToInt32(reader["Id"]);
                        cliente.Nome = reader["Nome"].ToString();
                        cliente.TipoCliente = reader["TipoCliente"].ToString();
                        cliente.CodiceFiscale = reader["CodiceFiscale"].ToString();
                        cliente.PartitaIva = reader["PartitaIva"].ToString();
                        // aggiungi cliente alla lista
                        clienti.Add(cliente);

                    }
                }
                catch (Exception ex)
                {
                    TempData["msgErrore"] = "Errore: " + ex.Message;
                }


            return clienti;
        }

        // Metodo per ottenere il cliente tramite id
        // Richiede il parametro id in formato intero
        // Restituisce un oggetto di tipo Cliente
        public Cliente GetClienteById(int id)
        {
            Cliente cliente = new Cliente();
            // ottieni connessione
            using (SqlConnection conn = Connection.GetConn())
                try
                {
                    conn.Open();
                    // crea comando
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Clienti WHERE Id = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    // esegui comando
                    SqlDataReader reader = cmd.ExecuteReader();
                    // leggi risultati
                    while (reader.Read())
                    {
                        // crea oggetto cliente
                        cliente.Id = Convert.ToInt32(reader["Id"]);
                        cliente.Nome = reader["Nome"].ToString();
                        cliente.TipoCliente = reader["TipoCliente"].ToString();
                        cliente.CodiceFiscale = reader["CodiceFiscale"].ToString();
                        cliente.PartitaIva = reader["PartitaIva"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData["msgErrore"] = "Errore: " + ex.Message;
                }

            return cliente;
        }

    }
}
