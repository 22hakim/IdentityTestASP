using RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;
using RunWepApp_withIdentity_TeddySmith_Youtube.Data;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;

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
    public async  Task<List<Club>> GetAllClubs()
    {
        var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
        var userClubs = _context.Clubs.Where(u => u.AppUserId == curUser);
        return userClubs.ToList();
    }

    public async Task<List<Races>> GetUserRaces()
    {
        var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
        var userRaces = _context.Races.Where(u => u.AppUserId == curUser);
        return userRaces.ToList();
    }
}
