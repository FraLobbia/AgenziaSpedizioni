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
        public string CodiceFiscale { get; set; }

        [Display(Name = "Partita Iva")]
        public string PartitaIva { get; set; }
    }
}