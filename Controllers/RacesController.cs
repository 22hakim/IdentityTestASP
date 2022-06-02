using Microsoft.AspNetCore.Mvc;
using RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Controllers;

public class RacesController : Controller
{
    private readonly IRaceRepository _ir;

    public RacesController(IRaceRepository raceRepository)
    {
        _ir = raceRepository;
    }
    public async Task<IActionResult>  Index()
    {
        IEnumerable<Races> listRaces = await _ir.GetAll();
        return View(listRaces);
    }

    public  async Task<IActionResult> Details(int id)
    {
        Races? race = await _ir.GetById(id);


        if(race is null)
        {
            return NotFound();
        }

        return View(race);
    }

    // GET: Races/Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Races race)
    {
        if (ModelState.IsValid)
        {
            _ir.Add(race);
            return RedirectToAction("Index");
        }
        return View(race);
    }
}
