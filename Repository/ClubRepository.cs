using RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;
using RunWepApp_withIdentity_TeddySmith_Youtube.Data;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;
using Microsoft.EntityFrameworkCore;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Repository;


public class ClubRepository : IClubRepository
{
    private readonly AppDBContext _db;

    public ClubRepository(AppDBContext appDB)
    {
        _db = appDB;
    }
    public bool Add(Club club)
    {
        _db.Add(club);
        return Save();
    }

    public bool Delete(Club club)
    {
        _db.Remove(club);
        return Save();
    }

    public async Task<IEnumerable<Club>> GetAll()
    {
        return await _db.Clubs.Include(c => c.Address).Include(c => c.AppUser).ToListAsync();
        
    }

    public async Task<Club> GetById(int id)
    {
        return await _db.Clubs.Include(c => c.Address)
                              .Include(c => c.AppUser)
                              .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Club>> GetClubsByCity(string city)
    {
        return await _db.Clubs.Where(club => club.Address.City.Contains(city)).ToListAsync();
    }

    public bool Save()
    {
        bool saved = _db.SaveChanges() > 0;
        return saved;
    }

    public bool Update(Club club)
    {
        _db.Update(club);
        return Save();
    }
}
