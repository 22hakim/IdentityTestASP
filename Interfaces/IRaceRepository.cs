using RunWepApp_withIdentity_TeddySmith_Youtube.Models;
namespace RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;

public interface IRaceRepository
{
    Task<IEnumerable<Races>> GetAll();

    Task<Races> GetByIdAsync(int id);

    Task<Races> GetByIdAsyncUntracked(int id);

    Task<IEnumerable<Races>> GetRacesByCity(string city);

    bool Add(Races race);

    bool Update(Races race);

    bool Delete(Races race);

    bool Save();
}
