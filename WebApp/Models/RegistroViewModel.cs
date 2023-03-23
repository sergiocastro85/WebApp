using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class RegistroViewModel
    {

        [Required(ErrorMessage ="El campo {0} es requerido")]
        [EmailAddress(ErrorMessage ="El campo debe ser un correo electrónico válido")]
        public string Email { get; set; }

        [Required(ErrorMessage ="El campo {0} es requerido")]
        [DataType(DataType.Password)]   
        public string pasword { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Tipo { get; set; }


    }
}
