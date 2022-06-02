using System.ComponentModel.DataAnnotations;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Models;

public class AppUser 
{
    [Key]
    public int Id { get; set; }
    public int Pace { get; set; }

    public int? Mileage { get; set; }

    public Address? Address { get; set; }

    public ICollection<Club>? Clubs { get; set; }

    public ICollection<Races>? Races { get; set; }

}
