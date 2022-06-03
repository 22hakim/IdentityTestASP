using RunWepApp_withIdentity_TeddySmith_Youtube.Data.Enum;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.ViewModels;

public class ClubViewModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int AddressId { get; set; }

    public Address Address { get; set;  }

    public IFormFile Image { get; set; }

    public string? URL { get; set; }

    public ClubCategory ClubCategory { get; set; }
}
