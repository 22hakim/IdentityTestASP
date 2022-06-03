using Microsoft.AspNetCore.Mvc;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;
using RunWepApp_withIdentity_TeddySmith_Youtube.ViewModels;
using RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Controllers;

public class ClubsController : Controller
{
    private readonly IClubRepository _cr;
    private readonly IPhotoService _ps;

    public ClubsController(IClubRepository clubRepository, IPhotoService photo)
    {
        _cr = clubRepository;
        _ps = photo;
    }

    // GET: Clubs
    public async Task<IActionResult> Index()
    {
        IEnumerable<Club> listClub = await _cr.GetAll();
        return View(listClub);
    }

    // GET: Clubs/Details/5
    public async Task<IActionResult> Details(int id)
    {
        Club? club = await _cr.GetByIdAsync(id);

        if(club is null)
        {
            return NotFound();
        }

        return View(club);
    }

    // GET: Clubs/Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClubViewModel clubModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _ps.AddPhotoAsync(clubModel.Image);
            Club club = new()
            {
                Title = clubModel.Title,
                Description = clubModel.Description,
                Image = result.Url.ToString(),
                Address = new Address
                {
                    City = clubModel.Address.City,
                    Street = clubModel.Address.Street,
                    State = clubModel.Address.State
                }
            };

            _cr.Add(club);
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("Image", "Photo upload failed");
        }
        return View(clubModel);
    }

    // GET: Clubs/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        Club? club = await _cr.GetByIdAsync(id);

        if (club is null)
        {
            return View("Error");
        }

        ClubViewModel clubVM = new()
        {
            Title = club.Title,
            Description = club.Description,
            AddressId = club.AddressId,
            Address = club.Address,
            URL = club.Image,
            ClubCategory = club.ClubCategory
        };

        return View(clubVM);
    }

    // POST: Clubs/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ClubViewModel clubVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to edit club");
            return View("Edit", clubVM);
        }

        Club club = await _cr.GetByIdAsyncUntracked(id);

        if(club is null)
        {
            ModelState.AddModelError("", "Failed to find club");
            return View("Edit", clubVM);
        }

        try
        {
            await _ps.DeletePhotoAsync(club.Image);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Could not delete photo");
            return View(clubVM);
        }

        var newPhoto = await _ps.AddPhotoAsync(clubVM.Image);

        Club editedClub = new()
        {
            Id = id,
            Title = clubVM.Title,
            Description = club.Description,
            Image = newPhoto.Url.ToString(),
            AddressId = clubVM.AddressId,
            Address = clubVM.Address,
            ClubCategory = clubVM.ClubCategory
        };

        _cr.Update(editedClub);

        return RedirectToAction("Index");
    }

    // GET: Clubs/Delete/5
    /*        public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _context.Clubs == null)
                {
                    return NotFound();
                }

                var club = await _context.Clubs
                    .Include(c => c.Address)
                    .Include(c => c.AppUser)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (club == null)
                {
                    return NotFound();
                }

                return View(club);
            }
    */
    // POST: Clubs/Delete/5
    /*        [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]*/
    /*        public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.Clubs == null)
                {
                    return Problem("Entity set 'AppDBContext.Clubs'  is null.");
                }
                var club = await _context.Clubs.FindAsync(id);
                if (club != null)
                {
                    _context.Clubs.Remove(club);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
    */
    /*        private bool ClubExists(int id)
            {
              return (_context.Clubs?.Any(e => e.Id == id)).GetValueOrDefault();
            }*/
}
