using System.ComponentModel.DataAnnotations;

namespace Ecommerce01.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }

        
        [Display(Name = "Pais")]
        [MaxLength(50,ErrorMessage ="El Campo {0} no debe de tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio")]
        public string Name { get; set; }

        //Propiedades de Navegacion
        public ICollection<State> States { get; set; }
        [Display(Name = "Provincias")]
        public int StatesNumber => States == null ? 0 : States.Count;

    }
}
