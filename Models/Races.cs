﻿using RunWepApp_withIdentity_TeddySmith_Youtube.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Models;


public class Races
{
    [Key]
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    [ForeignKey("Address")]
    public int AddressId { get; set; }

    public Address? Address { get; set; }

    public RaceCategory RaceCategory { get; set; }

    [ForeignKey("AppUser")]
    public string? AppUserId { get; set; }

    public AppUser? AppUser { get; set; }
}
