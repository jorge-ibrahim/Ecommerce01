using System.ComponentModel.DataAnnotations;

namespace Ecommerce01.Data.Entities
{
    public class State
    {
        public int Id { get; set; }


        [Display(Name = "Provincias")]
        [MaxLength(50, ErrorMessage = "El Campo {0} no debe de tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio")]
        public string Name { get; set; }

        //Propiedades de Navegacion
        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }

        [Display(Name = "Ciudades")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
    }
}
