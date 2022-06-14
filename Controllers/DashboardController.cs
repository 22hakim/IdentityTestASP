using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;
using RunWepApp_withIdentity_TeddySmith_Youtube.ViewModels;
using System.Security.Claims;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Controllers;

public class DashboardController : Controller
{
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPhotoService _photoService;
    private readonly IUserRepository _userRepository;

    public DashboardController(IDashboardRepository dashboardRepository, 
                               IHttpContextAccessor httpContextAccessor,
                               IPhotoService photoService, 
                               IUserRepository userRepository)
    {
        _dashboardRepository = dashboardRepository;
        _httpContextAccessor = httpContextAccessor;
        _photoService = photoService;
        _userRepository = userRepository;
    }

    private void EditMapUser(ref AppUser user, UserViewModel userViewModel, ImageUploadResult uploadResult)
    {
        user.Id = userViewModel.Id;
        user.Pace = userViewModel.Pace;
        user.Mileage = userViewModel.Mileage;
        user.ProfileImageUrl = uploadResult.Url.ToString();
        user.City = userViewModel.City;
        user.Street = userViewModel.Street;
        user.State = userViewModel.State;
    }
    public async Task<IActionResult> Index()
    {
        DashboardViewModel dashboardViewModel = new()
        {
            Races = await _dashboardRepository.GetUserRaces(),
            Clubs = await _dashboardRepository.GetAllClubs()
        };
        return View(dashboardViewModel);
    }

    public async Task<IActionResult> EditUserProfil()
    {
        string curUserID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var user = await _dashboardRepository.GetAppUserById(curUserID);
        if (user is null) return View("Error");

        UserViewModel uvm = new()
        {
            Id = user.Id,
            Pace = user.Pace,
            Mileage = user.Mileage,
            ProfileImageUrl = user.ProfileImageUrl,
            Street = user.Street,
            City = user.City,
            State = user.State
        };
        return View(uvm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUserProfil(UserViewModel userViewModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to edit profile");
            return View("EditUserProfil", userViewModel);
        }


        AppUser user = await _userRepository.GetByIdAsyncUntracked(userViewModel.Id);

        if (user is null)
        {
            ModelState.AddModelError("", "Failed to find user");
            return View("EditUserProfil", userViewModel);
        }

        if(user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
        {
            ImageUploadResult photoResult = await _photoService.AddPhotoAsync(userViewModel.Image);

            // pour éviter un truc qui peut arriver *something like : tracking error* on appelle une methode interne
            EditMapUser(ref user, userViewModel, photoResult);

            _userRepository.Update(user);

            return RedirectToAction("Index");

        }
        else
        {
            try
            {
                await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Could not delete photo");
                return View(userViewModel);
            }

            ImageUploadResult photoResult = await _photoService.AddPhotoAsync(userViewModel.Image);

            EditMapUser(ref user, userViewModel, photoResult);

            _userRepository.Update(user);

            return RedirectToAction("Index");

        }
         
    }
}
