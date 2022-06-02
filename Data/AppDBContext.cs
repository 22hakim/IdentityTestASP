using Microsoft.EntityFrameworkCore;
using RunWepApp_withIdentity_TeddySmith_Youtube.Models;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {}

    public DbSet<Races>? Races { get; set; }

    public DbSet<Club>? Clubs { get; set; }

    public DbSet<Address>? Addresses { get; set; }
}
