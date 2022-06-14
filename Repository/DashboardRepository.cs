using RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;
using RunWepApp_withIdentity_TeddySmith_Youtube.Data;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;
using Microsoft.EntityFrameworkCore;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Repository;


public class DashboardRepository : IDashboardRepository
{
    private readonly AppDBContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DashboardRepository(AppDBContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    public Task<List<Club>> GetAllClubs()
    {
        string? curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
        IEnumerable<Club> userClubs = _context.Clubs.Where(u => u.AppUserId == curUser);
        return Task.FromResult(userClubs.ToList());
    }

    public Task<List<Races>> GetUserRaces()
    {
        var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
        var userRaces = _context.Races.Where(u => u.AppUserId == curUser);
        return Task.FromResult(userRaces.ToList());
    }

    public async Task<AppUser> GetAppUserById(string id)
    {
        return await _context.Users.Include(c => c.Address).FirstOrDefaultAsync(i => i.Id == id);
    }
}
