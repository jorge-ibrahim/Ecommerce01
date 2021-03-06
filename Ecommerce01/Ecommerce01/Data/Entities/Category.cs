using System.ComponentModel.DataAnnotations;

namespace Ecommerce01.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }


        [Display(Name = "Categoria")]
        [MaxLength(50, ErrorMessage = "El Campo {0} no debe de tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio")]
        public string Name { get; set; }
    }
}
