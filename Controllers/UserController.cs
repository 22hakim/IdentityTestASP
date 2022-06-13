using Microsoft.AspNetCore.Mvc;
using RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;
using RunWepApp_withIdentity_TeddySmith_Youtube.ViewModels;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Controllers;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository =  userRepository;
    }
    [HttpGet("users")]
    public async Task<IActionResult> Index()
    {
        var users = await _userRepository.GetAllUsers();
        List<UserViewModel> listUsers = new List<UserViewModel>();
        foreach (var user in users)
        {
            UserViewModel uvm = new()
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Mileage = user.Mileage
            };
            listUsers.Add(uvm);
        }
        return View(listUsers);
    }

    public async Task<IActionResult> Details(string id)
    {
        var user = await _userRepository.GetUserById(id);
        UserViewModel uvm = new()
        {
            Id = user.Id,
            UserName = user.UserName,
            Pace = user.Pace,
            Mileage = user.Mileage
        };

        return View(uvm);
    }
}
