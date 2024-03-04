using System;
using System.ComponentModel.DataAnnotations;

namespace AgenziaSpedizioni.Models
{
    public class Aggiornamenti : Spedizione
    {
        [ScaffoldColumn(false)]
        public int IdAggiornamento { get; set; }

        [Required(ErrorMessage = "Il campo Stato è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il campo Stato non può superare i 50 caratteri.")]
        [RegularExpression("^(in transito|in consegna|consegnato|non consegnato)$", ErrorMessage = "Il campo Stato deve essere uno dei seguenti: 'in transito', 'in consegna', 'consegnato', 'non consegnato'.")]
        public string Stato { get; set; }

        [StringLength(100, ErrorMessage = "Il campo LuogoPacco non può superare i 100 caratteri.")]
        [Display(Name = "Luogo Pacco")]
        public string LuogoPacco { get; set; }

        [StringLength(255, ErrorMessage = "Il campo Descrizione non può superare i 255 caratteri.")]
        public string Descrizione { get; set; }

        [Required(ErrorMessage = "Il campo DataAggiornamento è obbligatorio.")]
        [Display(Name = "Data e Ora Aggiornamento")]
        [DataType(DataType.DateTime)]
        public DateTime DataOraAggiornamento { get; set; }

    }
}