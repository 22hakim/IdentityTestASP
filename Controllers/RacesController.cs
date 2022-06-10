using Microsoft.AspNetCore.Mvc;
using RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;
using RunWepApp_withIdentity_TeddySmith_Youtube.ViewModels;
using System.Security.Claims;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Controllers;

public class RacesController : Controller
{
    private readonly IRaceRepository _ir;
    private readonly IPhotoService _ps;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RacesController(IRaceRepository raceRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
    {
        _ir = raceRepository;
        _ps = photoService;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<IActionResult>  Index()
    {
        IEnumerable<Races> listRaces = await _ir.GetAll();
        return View(listRaces);
    }

    public  async Task<IActionResult> Details(int id)
    {
        Races? race = await _ir.GetByIdAsync(id);


        if(race is null)
        {
            return NotFound();
        }

        return View(race);
    }

    // GET: Races/Create
    public IActionResult Create()
    {
        string? userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        RaceViewModel raceViewModel = new() { AppUserId = userId };
        return View(raceViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RaceViewModel raceModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _ps.AddPhotoAsync(raceModel.Image);
            Races race = new()
            {
                Title = raceModel.Title,
                Description = raceModel.Description,
                Image = result.Url.ToString(),
                AppUserId = raceModel.AppUserId,
                Address = new Address
                {
                    City = raceModel.Address.City,
                    Street = raceModel.Address.Street,
                    State = raceModel.Address.State
                },
            };

            _ir.Add(race);
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("Image", "Photo upload failed");
        }
        return View(raceModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        Races? race = await _ir.GetByIdAsync(id);

        if (race is null)
        {
            return View("Error");
        }

        RaceViewModel raceVM = new()
        {
            Title = race.Title!,
            Description = race.Description!,
            AddressId = race.AddressId,
            Address = race.Address!,
            URL = race.Image,
            RaceCategory = race.RaceCategory
        };

        return View(raceVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, RaceViewModel raceVM)
    {
        if(!ModelState.IsValid)
        {
            ModelState.AddModelError("", "failed to edit race");
            return View("Edit", raceVM);
        }

        Races? race = await _ir.GetByIdAsyncUntracked(id);

        if (race is null)
        {
            ModelState.AddModelError("", "Failed to find race");
            return View("Edit", raceVM);
        }

        try
        {
            await _ps.DeletePhotoAsync(race.Image);
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "could not change photo");
            return View("Edit", raceVM);
        }

        var newPhoto = await _ps.AddPhotoAsync(raceVM.Image);
        
        Races editedRace = new()
        {
            Id = id,
            Title = raceVM.Title,
            Description = raceVM.Description,
            Image = newPhoto.Url.ToString(),
            AddressId = raceVM.AddressId,
            Address = raceVM.Address,
            RaceCategory = raceVM.RaceCategory 
        };

        _ir.Update(editedRace);

        return RedirectToAction("Index");

    }

    public async Task<IActionResult> Delete(int id)
    {
        Races? race = await _ir.GetByIdAsync(id);

        if (race is null)
        {
            ErrorViewModel error = new("erreur de ouf");
            return View("Error", error);
        }

        return View(race);
    }

    // POST: Races/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Races race)
    {
        if (race.Id == 0)
        {
            return Problem("Entity set 'AppDBContext.Races'  is null.");
        }

        Races Deletedrace = await _ir.GetByIdAsync(race.Id);

        if (Deletedrace is not null)
        {
            _ir.Delete(Deletedrace);
        }
        return RedirectToAction("Index");
    }
}
