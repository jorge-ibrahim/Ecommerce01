using System.ComponentModel.DataAnnotations;

namespace Ecommerce01.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debes ingresar un correo válido.")]//valida que la cadena ingresada sea del formato de email
        public string Username { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener al menos {1} carácteres.")]
        public string Password { get; set; }

        [Display(Name = "Recordarme en este navegador")]
        public bool RememberMe { get; set; }

    }
}
