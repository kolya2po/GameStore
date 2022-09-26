using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models.Identity
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "You should provide your first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You should provide your last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You should provide your username.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You should provide your email.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "You should provide your password.")]
        public string Password { get; set; }
    }
}
