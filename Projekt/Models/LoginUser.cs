using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class LoginUser
    {

        [Required(ErrorMessage = "Prosze podaj nazwę użytkownika")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Prosze podaj hasło")]
        public string Password { get; set; }


    }
}
