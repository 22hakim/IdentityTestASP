using RunWepApp_withIdentity_TeddySmith_Youtube.Models;
namespace RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;

public interface IClubRepository
{
    Task<IEnumerable<Club>> GetAll();

    Task<Club> GetById(int id);

    Task<IEnumerable<Club>> GetClubsByCity(string city);

    bool Add(Club club);

    bool Update(Club club);

    bool Delete(Club club);

    bool Save();
}
