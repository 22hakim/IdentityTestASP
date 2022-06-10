using Microsoft.AspNetCore.Mvc;
using RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;
using RunWepApp_withIdentity_TeddySmith_Youtube.ViewModels;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Controllers;

public class DashboardController : Controller
{
    private readonly IDashboardRepository _dashboardRepository;

    public DashboardController(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
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
}
