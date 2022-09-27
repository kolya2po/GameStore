using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models.Identity
{
    public class LoginModel
    {
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }
}
