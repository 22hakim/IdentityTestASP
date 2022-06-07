using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Data;

// dans IdentityDBC<???> on remplacera les ??? par les classes qui utiliserons vraiment identity ( user, role , ...)
public class AppDBContext : IdentityDbContext<AppUser>
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {}

    public DbSet<Races>? Races { get; set; }

    public DbSet<Club>? Clubs { get; set; }

    public DbSet<Address>? Addresses { get; set; }
}
