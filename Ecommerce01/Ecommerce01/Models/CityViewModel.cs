using System.ComponentModel.DataAnnotations;

namespace Ecommerce01.Models
{
    public class CityViewModel
    {

        public int Id { get; set; }


        [Display(Name = "Ciudad")]
        [MaxLength(50, ErrorMessage = "El Campo {0} no debe de tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio")]
        public string Name { get; set; }

        //Propiedades de Navegacion
        public int StateId { get; set; }

    }
}
