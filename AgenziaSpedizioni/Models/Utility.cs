using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AgenziaSpedizioni.Models
{
    public static class Utility
    {
        // Metodo per ottenere la lista dei clienti dal db dalla tabella Cliente
        // Non richiede parametri
        // Restituisce una lista di oggetti di tipo Cliente
        public static List<Cliente> GetListaClienti()
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
                    Cliente msgErrore = new Cliente();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    clienti.Add(msgErrore);
                }
            return clienti;
        }

        // Metodo per ottenere il cliente tramite id
        // Richiede il parametro id in formato intero
        // Restituisce un oggetto di tipo Cliente
        public static Cliente GetClienteById(int id)
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
                    Cliente msgErrore = new Cliente();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    return msgErrore;
                }

            return cliente;
        }

        // Meotodo per ottenere la lista delle spedizioni
        // Non riceve parametri
        // Restituisce una lista di oggetti Spedizione
        public static List<Spedizione> GetListaSpedizioni()
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
                    Spedizione msgErrore = new Spedizione();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    spedizioni.Add(msgErrore);
                }
            }

            return spedizioni;
        }

        // Metodo per ottenere una spedizione tramite id
        // Riceve un intero id
        // Restituisce un oggetto Spedizione
        public static Spedizione GetSpedizioneById(int id)
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
                    Spedizione msgErrore = new Spedizione();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    return msgErrore;
                }
            }

            return spedizione;
        }

        // Metodo per ottenere una spedizione tramite NumeroIdentificativo
        // Riceve una stringa NumeroIdentificativo
        // Restituisce un oggetto Spedizione
        public static Spedizione GetSpedizioneByNumeroIdentificativo(string numeroIdentificativo)
        {
            // crea oggetto spedizione
            Spedizione spedizione = new Spedizione();

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    // crea comando
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Spedizioni WHERE NumeroIdentificativo = @NumeroIdentificativo", conn);
                    cmd.Parameters.AddWithValue("@NumeroIdentificativo", numeroIdentificativo);
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
                    Spedizione msgErrore = new Spedizione();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    return msgErrore;
                }
            }

            return spedizione;
        }

        // Metodo per ottenere id spedizione tramite NumeroIdentificativo
        // Riceve una stringa NumeroIdentificativo
        // Restituisce un intero id
        public static int GetIdSpedizioneByNumeroIdentificativo(string numeroIdentificativo)
        {
            int id = 0;

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    // crea comando
                    SqlCommand cmd = new SqlCommand("SELECT Id FROM Spedizioni WHERE NumeroIdentificativo = @NumeroIdentificativo", conn);
                    cmd.Parameters.AddWithValue("@NumeroIdentificativo", numeroIdentificativo);
                    // esegui comando
                    SqlDataReader reader = cmd.ExecuteReader();
                    // leggi risultati
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader["Id"]);
                    }
                }
                catch (Exception ex)
                {
                    id = -1; // -1 indica errore
                }
            }

            return id;
        }

        // Metodo per ottenere la lista degli aggiornamenti tramite id spedizione
        // Riceve un intero idSpedizione
        // Restituisce una lista di oggetti Aggiornamenti
        public static List<Aggiornamenti> GetListaAggiornamenti(int idSpedizione)
        {
            // crea lista di aggiornamenti
            List<Aggiornamenti> aggiornamenti = new List<Aggiornamenti>();

            using (SqlConnection conn = Connection.GetConn())
            {
                try
                {
                    conn.Open();
                    // crea comando
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Aggiornamenti WHERE IdSpedizione = @IdSpedizione", conn);
                    cmd.Parameters.AddWithValue("@IdSpedizione", idSpedizione);
                    // esegui comando
                    SqlDataReader reader = cmd.ExecuteReader();
                    // leggi risultati
                    while (reader.Read())
                    {
                        // crea oggetto aggiornamento
                        Aggiornamenti aggiornamento = new Aggiornamenti();
                        aggiornamento.Id = Convert.ToInt32(reader["Id"]);
                        aggiornamento.IdAggiornamento = Convert.ToInt32(reader["IdAggiornamento"]);
                        aggiornamento.Stato = reader["Stato"].ToString();
                        aggiornamento.LuogoPacco = reader["LuogoPacco"].ToString();
                        aggiornamento.Descrizione = reader["Descrizione"].ToString();
                        aggiornamento.DataOraAggiornamento = Convert.ToDateTime(reader["DataOraAggiornamento"]);

                        // aggiungi aggiornamento alla lista
                        aggiornamenti.Add(aggiornamento);
                    }
                }
                catch (Exception ex)
                {
                    Aggiornamenti msgErrore = new Aggiornamenti();
                    msgErrore.Messaggio = "Errore: " + ex.Message;
                    aggiornamenti.Add(msgErrore);
                }
            }

            return aggiornamenti;
        }

    }
}