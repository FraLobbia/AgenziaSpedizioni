using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AgenziaSpedizioni.Models
{
    public class Cliente
    {
        //     _____   _        _____   ______   _   _   _______   ______ 
        //    / ____| | |      |_   _| |  ____| | \ | | |__   __| |  ____|
        //   | |      | |        | |   | |__    |  \| |    | |    | |__   
        //   | |      | |        | |   |  __|   | . ` |    | |    |  __|  
        //   | |____  | |____   _| |_  | |____  | |\  |    | |    | |____ 
        //    \_____| |______| |_____| |______| |_| \_|    |_|    |______|

        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Il campo Nome è obbligatorio.")]
        [Remote("IsNomeClienteAvailable", "Cliente", ErrorMessage = "Il nome del cliente è già presente, inserirne un altro.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il campo Tipo Cliente è obbligatorio.")]
        [Display(Name = "Tipo Cliente")]
        [RegularExpression("^(Privato|Azienda)$", ErrorMessage = "Il campo tipo del cliente deve essere uno dei seguenti: 'Privato', 'Azienda'.")]
        public string TipoCliente { get; set; }


        [Display(Name = "Codice Fiscale")]
        [StringLength(16, ErrorMessage = "Il campo CodiceFiscale non può superare i 16 caratteri.")]
        [Remote("IsCodiceFiscaleAvailable", "Cliente", ErrorMessage = "Il codice fiscale del cliente è già presente, inserirne un altro.")]
        public string CodiceFiscale { get; set; }

        [Display(Name = "Partita Iva")]
        [StringLength(11, ErrorMessage = "Il campo PartitaIva non può superare i 11 caratteri.")]
        //[Remote("IsPartitaIvaClienteAvailable", "Cliente", ErrorMessage = "La partita iva del cliente è già presente, inserirne un'altra.")]
        public string PartitaIva { get; set; }

    }
}