using System.ComponentModel.DataAnnotations;

namespace BookStorage.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập username")]
        public string Username { set; get; }
        [Required(ErrorMessage = "Mời nhập password")]
        public string Password { set; get; }
    }
}