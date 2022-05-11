using Ecommerce01.Data;
using Ecommerce01.Data.Entities;
using Ecommerce01.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce01.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _rolManager;
        private readonly SignInManager<User> _singInManager;

        //UserManager<User> clase que brinda EF para manejar todo de los usuarios
        // RoleManager<IdentityRole> clase que brinda EF para manejar todo sobre los roles
        public UserHelper(DataContext context,UserManager<User> userManager,RoleManager<IdentityRole> rolManager, SignInManager<User> singInManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._rolManager = rolManager;
            this._singInManager = singInManager;
            
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _rolManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _rolManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }

        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users
            .Include(u => u.City)
            .FirstOrDefaultAsync(u => u.Email == email);

        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);

        }

        public async Task<SignInResult> LoginAsync(LoginViewModel entidad)
        {
            //con el false de abajo es para bloquear la funcion que verifica hasta 3 intentos de ingreso
            return await _singInManager.PasswordSignInAsync(entidad.Username, entidad.Password, entidad.RememberMe, false);//
        }

        public async Task LogoutAsync()
        {
            await _singInManager.SignOutAsync();
        }
    }
}
