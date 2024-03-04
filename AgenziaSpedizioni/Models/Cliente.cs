using System.ComponentModel.DataAnnotations;

namespace AgenziaSpedizioni.Models
{
    public class Cliente
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Il campo Nome è obbligatorio.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il campo Tipo Cliente è obbligatorio.")]
        [Display(Name = "Tipo Cliente")]
        public string TipoCliente { get; set; }


        [Display(Name = "Codice Fiscale")]
        [StringLength(16, ErrorMessage = "Il campo CodiceFiscale non può superare i 16 caratteri.")]
        public string CodiceFiscale { get; set; }

        [Display(Name = "Partita Iva")]
        [StringLength(11, ErrorMessage = "Il campo PartitaIva non può superare i 11 caratteri.")]
        public string PartitaIva { get; set; }
    }
}