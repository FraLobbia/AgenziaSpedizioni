
using System.ComponentModel.DataAnnotations;


namespace AgenziaSpedizioni.Models
{
    public class Utente
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}