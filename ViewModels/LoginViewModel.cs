using System.ComponentModel.DataAnnotations;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email address is needed")]
    [Display(Name ="Email Address")]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
