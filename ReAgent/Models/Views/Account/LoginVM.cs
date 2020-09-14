using System.ComponentModel.DataAnnotations;

namespace ReAgent.Models.Views.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Поле \"{0}\" обязательное")]
        [EmailAddress(ErrorMessage = "Неверный формат")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле \"{0}\" обязательное")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}