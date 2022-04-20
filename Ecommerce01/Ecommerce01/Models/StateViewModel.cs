using System.ComponentModel.DataAnnotations;

namespace Ecommerce01.Models
{
    public class StateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Provincia")]
        [MaxLength(50,ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }
        public int CountryId { get; set; }
    }
}
