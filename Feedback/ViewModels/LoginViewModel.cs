using System.ComponentModel.DataAnnotations;

namespace Feedback.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Error")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Error")]
        public string Password { get; set; }
    }
}
