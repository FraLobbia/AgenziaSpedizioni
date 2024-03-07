using AgenziaSpedizioni.Models;
using System.Collections.Generic;
using System.Web.Mvc;
namespace AgenziaSpedizioni.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CheckStatus()
        {
            // mostra solo un imput in cui inserire il numero identificativo
            return View();
        }

        [HttpPost]
        public ActionResult CheckStatus(FormCollection collection)
        {
            // ottengo il numero identificativo e il codice fiscale/partita iva dal form
            string NumeroIdentificativo = collection["NumeroIdentificativo"];
            string codFiscPartIVA = collection["CodFiscPartIVA"];

            // salvo i valori inseriti in TempData per poterli recuperare nella view successiva ed evitare che venga bypassato il form
            TempData["NumeroIdentificativo"] = NumeroIdentificativo;
            TempData["CodFiscPartIVA"] = codFiscPartIVA;

            // controllo se esiste una spedizione con quel numero identificativo e quel codice fiscale/partita iva
            int id = Utility.CheckSpedizione(NumeroIdentificativo, codFiscPartIVA); // il metodo restituisce 0 se la spedizione non esiste, -1 se si è verificato un errore, n. dell'ID se la spedizione esiste

            // gestisco i casi in cui la spedizione non esiste o il numero identificativo non è valido o non esiste
            switch (id)
            {
                case 0:
                    TempData["msgErrore"] = "I valori inseriti non sono validi";
                    return View();
                case -1:
                    TempData["msgErrore"] = "Si è verificato un errore";
                    return View();
            }

            return RedirectToAction("Status", "Home", new { id = id });
        }

        public ActionResult Status(int id)
        {
            // controllo che non sia stato bypassato il form di inserimento del numero identificativo altrimenti reindirizzo alla pagina di inserimento
            if (TempData["NumeroIdentificativo"] == null || TempData["CodFiscPartIVA"] == null)
                return RedirectToAction("CheckStatus", "Home");

            // se arrivo qui, significa che il form è stato compilato correttamente e ottengo i valori inseriti
            string NumeroIdentificativo = TempData["NumeroIdentificativo"].ToString();
            string CodFiscPartIVA = TempData["CodFiscPartIVA"].ToString();

            // controllo che il numero identificativo e il codice fiscale/partita iva siano validi e combacino nel DB con il metodo CheckSpedizione
            int confronto = Utility.CheckSpedizione(NumeroIdentificativo, CodFiscPartIVA); // il metodo restituisce 0 se la spedizione non esiste, -1 se si è verificato un errore, n. dell'ID se la spedizione esiste

            if (confronto == 0 || confronto == -1)
                return RedirectToAction("CheckStatus", "Home");

            // ottengo la spedizione specifica e la salvo in TempData per poterla visualizzare nella view
            Spedizione spedizione = Utility.GetSpedizioneById(id);
            TempData["Spedizione"] = spedizione;

            // ottengo la lista degli aggiornamenti relativi a quella spedizione e la salvo in TempData per poterla visualizzare nella view
            List<Aggiornamenti> aggiornamenti = Utility.GetListaAggiornamenti(id);
            return View(aggiornamenti);
        }

    }
}