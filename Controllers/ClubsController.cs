using Microsoft.AspNetCore.Mvc;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;
using RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Controllers;

public class ClubsController : Controller
{
    private readonly IClubRepository _cr;

    public ClubsController(IClubRepository clubRepository)
    {
        _cr = clubRepository;
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
        Club? club = await _cr.GetById(id);

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
    public IActionResult Create(Club club)
    {
        if (ModelState.IsValid)
        {
            _cr.Add(club);
            return RedirectToAction("Index");
        }
        return View(club);
    }

    // GET: Clubs/Edit/5
    /*        public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.Clubs == null)
                {
                    return NotFound();
                }

                var club = await _context.Clubs.FindAsync(id);
                if (club == null)
                {
                    return NotFound();
                }
                ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", club.AddressId);
                ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", club.AppUserId);
                return View(club);
            }*/

    // POST: Clubs/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    /*        [HttpPost]
            [ValidateAntiForgeryToken]*/
    /*        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Image,AddressId,ClubCategory,AppUserId")] Club club)
            {
                if (id != club.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(club);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClubExists(club.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", club.AddressId);
                ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", club.AppUserId);
                return View(club);
            }
    */
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
