using RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;
using RunWepApp_withIdentity_TeddySmith_Youtube.Data;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;
using Microsoft.EntityFrameworkCore;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Repository;


public class RaceRepository : IRaceRepository
{
    private readonly AppDBContext _db;

    public RaceRepository(AppDBContext appDB)
    {
        _db = appDB;
    }
    public bool Add(Races race)
    {
        _db.Add(race);
        return Save();
    }

    public bool Delete(Races race)
    {
        _db.Remove(race);
        return Save();
    }

    public async Task<IEnumerable<Races>> GetAll()
    {
        return await _db.Races.ToListAsync();
    }

    public async Task<Races> GetByIdAsync(int id)
    {
        return await _db.Races.Include(race => race.Address)
                              .Include(race => race.AppUser)
                              .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Races> GetByIdAsyncUntracked(int id)
    {
        return await _db.Races.AsNoTracking().Include(race => race.Address)
                              .Include(race => race.AppUser)
                              .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Races>> GetRacesByCity(string city)
    {
        return await _db.Races.Where(race => race.Address.City.Contains(city)).ToListAsync();
    }

    public bool Save()
    {
        return _db.SaveChanges() > 0;
    }

    public bool Update(Races race)
    {
        _db.Update(race);
        return Save();
    }
}
