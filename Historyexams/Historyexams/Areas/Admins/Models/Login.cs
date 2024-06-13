
using System.ComponentModel.DataAnnotations;

namespace Historyexams.Areas.Admins.Models
{
    public class Login
    {
        [Required(ErrorMessage ="UserName không để trống")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không để trống")]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
