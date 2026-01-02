using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class User
    {

        [Key]
        public int Id { set; get; }

        [Required(ErrorMessage = "Prosze podaj Imie")]
        [MinLength(2, ErrorMessage = "Imie musi mieć conajmniej dwa znaki.")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Prosze podaj Nazwisko")]
        [MinLength(2, ErrorMessage = "Nazwisko musi mieć conajmniej dwa znaki.")]
        public string Surname { set; get; }

        [Required(ErrorMessage = "Prosze podaj nazwę użytkownika")]
        [MinLength(2, ErrorMessage = "Nazwa użytkownika musi mieć conajmniej dwa znaki.")]
        public string Username { set; get; }

        [Required(ErrorMessage = "Prosze podaj E-mail")]
        [EmailAddress(ErrorMessage ="Niepoprawny format adresu E-mail.")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Prosze podaj Hasło")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage ="Hasło musi zawierać conajmniej 8 znaków złożonych z małych liter, wielkich liter oraz cyfr.")]
        public string Password { set; get; }

        [NotMapped]
        [Required(ErrorMessage = "Potwierdź swoje Hasło")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła muszą być identyczne.")]
        public string RepeatPassword { set; get; }


        [Required(ErrorMessage = "Prosze podaj numer telefonu")]
        [Phone(ErrorMessage = "Niepoprawny format numeru telefonu.")]
        public string Number { set; get; }

        [Required(ErrorMessage = "Prosze podaj wiek")]
        [Range(10, 80, ErrorMessage = "Wiek musi być z zakresu 10-80.")]
        public int? Age { set; get; }

        [Required(ErrorMessage = "Wybierz miasto.")]
        public Miasta Town { get; set; }

        public enum Miasta
        {
            Kraków  = 1,
            Warszawa = 2,
            Gdańsk = 3,
            Tarnów = 4,
            Poznań = 5
        }

        public int SuperUser { set; get; } = 0;

    }
}
