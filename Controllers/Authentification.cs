using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunWepApp_withIdentity_TeddySmith_Youtube.Data;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;
using RunWepApp_withIdentity_TeddySmith_Youtube.ViewModels;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Controllers;

public class Authentification : Controller
{
    readonly UserManager<AppUser> _userManager;
    readonly SignInManager<AppUser> _signInManager;

    public Authentification(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {
        LoginViewModel response = new();
        return View(response);
    }

/*        [HttpPost]
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
    }*/



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password)
    {
        LoginViewModel userInfo = new()
        {
            Email = email,
            Password = password
        };

        if (!ModelState.IsValid)
        {
            return View(userInfo);
        }

        AppUser? user = await _userManager.FindByEmailAsync(email);

        if (user is not null)
        {
            bool passwordCheck = await _userManager.CheckPasswordAsync(user, password);

            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Races");
                }
            }

        }
        TempData["message"] = "wrond credentials. Please try again";
        return View(userInfo);
    }


    public IActionResult Register()
    {
        RegisterViewModel response = new();
        return View(response);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel rvm)
    {
        if (!ModelState.IsValid) return View(rvm);

        AppUser? user = await _userManager.FindByEmailAsync(rvm.Email);

        if(user is not null)
        {
            TempData["message"] = "This email is already in use";
            return View(rvm);
        }

        AppUser newUser = new()
        {
            Email = rvm.Email,
            UserName = rvm.Email
        };

        IdentityResult newUserResponse = await _userManager.CreateAsync(newUser, rvm.Password);

        if (newUserResponse.Succeeded)
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index");
    }

}
