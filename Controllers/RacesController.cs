﻿using Microsoft.AspNetCore.Mvc;
using RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;
using RunWepApp_withIdentity_TeddySmith_Youtube.ViewModels;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Controllers;

public class RacesController : Controller
{
    private readonly IRaceRepository _ir;
    private readonly IPhotoService _ps;

    public RacesController(IRaceRepository raceRepository, IPhotoService photoService)
    {
        _ir = raceRepository;
        _ps = photoService;
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
        return View();
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
                Address = new Address
                {
                    City = raceModel.Address.City,
                    Street = raceModel.Address.Street,
                    State = raceModel.Address.State
                }
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
            Title = race.Title,
            Description = race.Description,
            AddressId = race.AddressId,
            Address = race.Address,
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

        Races race = await _ir.GetByIdAsync(id);

        if(race is null)
        {
            ModelState.AddModelError("", "Failed to find race");
            return View("Edit", raceVM);
        }

        try
        {
            await _ps.DeletePhotoAsync(race.Image);
        }
        catch (Exception ex)
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
}