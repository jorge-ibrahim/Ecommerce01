using Ecommerce01.Helpers;
using Ecommerce01.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce01.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;

        public AccountController(IUserHelper userHelper )
        {
            this._userHelper = userHelper;
        }

        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)//(User.Identity devuelve siempre el usuario logueado y autentica y lo envio al index del home
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginViewModel());//si no hay usuario logueado llamo al login
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel entidad)
        {
            if (ModelState.IsValid)//valido el modelo
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(entidad);
                if(result.Succeeded)//verifico si se pudo loguear
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Email o Contraseña incorrectos");
            }
            
            return View(entidad);
        }

        public async Task<IActionResult> Logout()//Deslogueo
        {
            await _userHelper.LogoutAsync();//borra la secion,credenciales,cookes y redirecciona al index
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NoAuthorized()
        {
            return View();
        }

    }
}
