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
        public ActionResult CheckStatus(string NumeroIdentificativo)
        {
            // ottieni l'id della spedizione tramite il numero identificativo
            int id = Utility.GetIdSpedizioneByNumeroIdentificativo(NumeroIdentificativo);
            // se l'id è -1, la spedizione non esiste
            if (id == -1)
            {
                ViewBag.msgErrore = "La spedizione con il numero identificativo " + NumeroIdentificativo + " non esiste.";
                return View();
            }
            // se invece esiste, reindirizza alla pagina Status del controller Spedizione
            return RedirectToAction("Status", "Spedizione", new { id = id });
        }

    }
}