using Ecommerce01.Data.Entities;
using Ecommerce01.Models;
using Microsoft.AspNetCore.Identity;


namespace Ecommerce01.Helpers
{
    public interface IUserHelper
    {

        Task<User> GetUserAsync(string email);//buscar usuario x email

        Task<IdentityResult> AddUserAsync(User user, string password);//crear usuarios

        Task CheckRoleAsync(string roleName);//verificar si un rol existe, si es asi lo creo

        Task AddUserToRoleAsync(User user, string roleName);//ve a que rol pertenese cada usuario

        Task<bool> IsUserInRoleAsync(User user, string roleName);//devuelve con un bool si un usuario es de un rol

        Task<SignInResult> LoginAsync(LoginViewModel entidad); //SignInResult es un objeto que me dise si se pudo loguear y si no pudo, el porque

        Task LogoutAsync();//metodo qeu se usa para ver si se ingresa y nada mas
    }
}
