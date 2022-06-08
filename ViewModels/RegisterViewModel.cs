using System.ComponentModel.DataAnnotations;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.ViewModels;

public class RegisterViewModel
{
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Email address is needed")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is needed")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Required(ErrorMessage = "Confirm password is needed")]
    [Compare("Password", ErrorMessage = "passwords do not match !")]
    public string ConfirmPassword { get; set; }
}
