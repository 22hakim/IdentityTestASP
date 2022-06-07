using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunWepApp_withIdentity_TeddySmith_Youtube.Data;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;
using RunWepApp_withIdentity_TeddySmith_Youtube.ViewModels;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Controllers
{
    public class Authentification : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly AppDBContext _context;

        public Authentification(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login()
        {
            LoginViewModel response = new();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            // vidéo 13 at 25:24
            if(!ModelState.IsValid)
            {
                return View(login);
            }
            
            AppUser? user = await _userManager.FindByEmailAsync(login.Email);

            if(user is not null)
            {
                bool passwordCheck = await _userManager.CheckPasswordAsync(user, login.Password);

                if(passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }

             }
            TempData["message"] = "wrond credentials. Please try again";
            return View(login);
        }
    }
}
