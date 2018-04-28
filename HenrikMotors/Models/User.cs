using System.ComponentModel.DataAnnotations;

namespace HenrikMotors.Models
{
    public class User
    {
        [Required]
        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig Wachtwoord")]
        [Compare("Password", ErrorMessage = "Wachtwoorden komen niet overeen.")]
        public string ConfirmPassword { get; set; }
    }
}