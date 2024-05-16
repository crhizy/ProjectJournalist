using Microsoft.AspNetCore.Mvc;
using ProjectJournalist.Models;
using Microsoft.EntityFrameworkCore;
using ProjectJournalist.ViewModels;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ProjectJournalist.Controllers
{
    public class AccessController : Controller
    {
        private readonly ApplicationDbContext _ApplicationDbContext;
        public AccessController(ApplicationDbContext ApplicationDbContext)
        {
            _ApplicationDbContext = ApplicationDbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserVM model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ViewData["Message"] = "The password are not the same";
                return View();
            }
            // Verificar si ya existe un usuario con el mismo correo electrónico
            var existingUser = await _ApplicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser != null)
            {
                ViewData["Message"] = "An account with this email already exists.";
                return View();
            }

            // Verificar si ya existe un usuario con el mismo nombre de usuario
            existingUser = await _ApplicationDbContext.Users.FirstOrDefaultAsync(u => u.Name == model.Name);
            if (existingUser != null)
            {
                ViewData["Message"] = "An account with this username already exists.";
                return View();
            }

            User user = new User()
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password
            };
            await _ApplicationDbContext.Users.AddAsync(user);
            await _ApplicationDbContext.SaveChangesAsync();


            if (user.Id != 0) return RedirectToAction("Login", "Access");

            ViewData["Message"] = "The user was not created, fatal error";
            return View();

        }
        [HttpGet]
        public IActionResult logIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserVM model)
        {
            User? userFound = await _ApplicationDbContext.Users.Where(
                u =>u.Email == model.Email && u.Password == model.Password).FirstOrDefaultAsync();
            if (userFound == null)
            {
                ViewData["Message"] = "No marches found";
                return View();
            }
            // Establecer el Id del usuario en la sesión
            HttpContext.Session.SetInt32("UserId", userFound.Id);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userFound.Name)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
             CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),properties
             );
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Access");
        }
    }
}
