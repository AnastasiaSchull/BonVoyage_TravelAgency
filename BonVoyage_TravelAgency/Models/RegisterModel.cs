using System.ComponentModel.DataAnnotations;

namespace BonVoyage_TravelAgency.Models
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserSurname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
