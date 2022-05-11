using Ecommerce01.Clases;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce01.Data.Entities
{
    public class User : IdentityUser
    {

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; }

        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Ciudad")]
        public City City { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Address { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }//Guid => es un codigo unico alfanumerico es como un token

        //TODO: Pending to put the correct paths/todo comentario TODO puede verse en la ventana de lista de tareas
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty//si no tiene foto el usuario
            ? $"https://localhost:7057/images/noimage.png"//asigno la cargada en el proyecto
            : $"https://shoppingprep.blob.core.windows.net/users/{ImageId}";//cuando la cargue en esta ruta la asignare

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }

        //propiedades de lectura

        [Display(Name = "Usuario")]//nombre completo
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Usuario")]//nombre completo con el documento
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";


    }
}
