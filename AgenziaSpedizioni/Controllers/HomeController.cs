using AgenziaSpedizioni.Models;
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


            // controllo se esiste una spedizione con quel numero identificativo e quel codice fiscale/partita iva
            int id = Utility.CheckSpedizione(NumeroIdentificativo, codFiscPartIVA);

            System.Diagnostics.Debug.WriteLine(id);
            // gestisci i casi in cui la spedizione non esiste o il numero identificativo non è valido o non esiste
            switch (id)
            {

                case 0:
                    TempData["msgErrore"] = "I valori inseriti non sono validi";
                    return View();
                case -1:
                    TempData["msgErrore"] = "Si è verificato un errore";
                    return View();
            }

            return RedirectToAction("Status", "Spedizione", new { id = id });
        }

    }
}