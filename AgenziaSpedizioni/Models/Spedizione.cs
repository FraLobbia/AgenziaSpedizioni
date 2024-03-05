using System;
using System.ComponentModel.DataAnnotations;

namespace AgenziaSpedizioni.Models
{
    public class Spedizione
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo Nome Cliente è obbligatorio.")]
        [Display(Name = "Nome Cliente")]
        public int ClienteId { get; set; } // Da impostare come dropdown

        [Required(ErrorMessage = "Il campo NumeroIdentificativo è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il campo NumeroIdentificativo non può superare i 50 caratteri.")]
        [Display(Name = "Numero Identificativo")]
        public string NumeroIdentificativo { get; set; }

        [Required(ErrorMessage = "Il campo DataSpedizione è obbligatorio.")]
        [Display(Name = "Data Spedizione")]
        [DataType(DataType.DateTime)]
        public DateTime DataSpedizione { get; set; }

        [Required(ErrorMessage = "Il campo Peso è obbligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Il campo Peso deve essere maggiore di zero.")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "Il campo CittaDestinataria è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il campo CittaDestinataria non può superare i 100 caratteri.")]
        [Display(Name = "Città Destinataria")]
        public string CittaDestinataria { get; set; }

        [Required(ErrorMessage = "Il campo IndirizzoDestinatario è obbligatorio.")]
        [StringLength(255, ErrorMessage = "Il campo IndirizzoDestinatario non può superare i 255 caratteri.")]
        [Display(Name = "Indirizzo Destinatario")]
        public string IndirizzoDestinatario { get; set; }

        [Required(ErrorMessage = "Il campo NominativoDestinatario è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il campo NominativoDestinatario non può superare i 100 caratteri.")]
        [Display(Name = "Nominativo Destinatario")]
        public string NominativoDestinatario { get; set; }

        [Required(ErrorMessage = "Il campo Costo è obbligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Il campo Costo deve essere maggiore di zero.")]
        public decimal Costo { get; set; }

        [Required(ErrorMessage = "Il campo DataConsegnaPrevista è obbligatorio.")]
        [Display(Name = "Data Consegna Prevista")]
        [DataType(DataType.DateTime)]
        public DateTime DataConsegnaPrevista { get; set; }

    }
}
