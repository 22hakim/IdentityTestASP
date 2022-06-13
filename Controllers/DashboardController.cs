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

    public DashboardController(IDashboardRepository dashboardRepository, 
                               IHttpContextAccessor httpContextAccessor,
                               IPhotoService photoService)
    {
        _dashboardRepository = dashboardRepository;
        _httpContextAccessor = httpContextAccessor;
        _photoService = photoService;
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
        var user = await _dashboardRepository.GetAppUserByIs(curUserID);
        if (user is null) return View("Error");

        if(user.Address is null)
        {
            user.Address = new Address()
            {
                City = "undefined",
                State = "undefined",
            };
        }
        UserViewModel uvm = new()
        {
            Id = user.Id,
            Pace = user.Pace,
            Mileage = user.Mileage,
            ProfileImageUrl = user.ProfileImageUrl,
            City = user.Address.City ?? null,
            State = user.Address.State! ?? null
        };
        return View(uvm);
    }
}
