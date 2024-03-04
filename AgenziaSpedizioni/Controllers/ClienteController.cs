using AgenziaSpedizioni.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AgenziaSpedizioni.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            // ottengo la lista dei clienti
            List<Cliente> clienti = GetClientiPrivato();
            ViewBag.msgSuccess = TempData["msgSuccess"];
            return View(clienti);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    // crea comando
                    SqlCommand cmd = new SqlCommand("" +
                        "INSERT INTO Clienti " +
                        "(Nome, TipoCliente, CodiceFiscale, PartitaIva) " +
                        "VALUES (@Nome, @TipoCliente, @CodiceFiscale, @PartitaIva)", conn);

                    // aggiungi parametri
                    cmd.Parameters.AddWithValue("@Nome", collection["Nome"]);
                    cmd.Parameters.AddWithValue("@TipoCliente", collection["TipoCliente"]);
                    cmd.Parameters.AddWithValue("@CodiceFiscale", collection["CodiceFiscale"]);
                    cmd.Parameters.AddWithValue("@PartitaIva", collection["PartitaIva"]);

                    // esegui comando
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    ViewBag.msgErrore = "Errore: " + ex.Message;
                    return View(collection);
                }
                finally
                {
                    conn.Close();
                }

                TempData["msgSuccess"] = "Cliente" + collection["Nome"] + " inserito correttamente";
                return RedirectToAction("Index");
            }

        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cliente/Edit/5
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

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cliente/Delete/5
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

        // Metodo per ottenere la lista dei clienti dal db dalla tabella Cliente
        // Non richiede parametri
        // Restituisce una lista di oggetti di tipo Cliente
        public List<Cliente> GetClientiPrivato()
        {
            // crea lista di clienti
            List<Cliente> clienti = new List<Cliente>();
            // ottieni connessione
            SqlConnection conn = Connection.GetConn();

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
                ViewBag.Message = "Errore: " + ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return clienti;
        }

    }
}
