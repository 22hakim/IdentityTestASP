using RunWepApp_withIdentity_TeddySmith_Youtube.Models;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<AppUser>> GetAllUsers();

    Task<AppUser> GetUserById(string id);

    Task<AppUser> GetByIdAsyncUntracked(string id);

    bool Add(AppUser user);

    bool Update(AppUser user);

    bool Delete(AppUser user);

    bool Save();
}
