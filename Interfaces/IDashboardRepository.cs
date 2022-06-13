using RunWepApp_withIdentity_TeddySmith_Youtube.Models;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;

public interface IDashboardRepository
{
    Task<List<Club>> GetAllClubs();
    Task<List<Races>> GetUserRaces();

    Task<AppUser> GetAppUserByIs(string id);
}
