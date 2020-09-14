using System.ComponentModel.DataAnnotations;

namespace ReAgent.Models.Views.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Поле \"{0}\" обязательное")]
        [EmailAddress(ErrorMessage = "Неверный формат")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле \"{0}\" обязательное")]
        [Display(Name = "Имя пользователя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле \"{0}\" обязательное")]
        [Display(Name = "Фамилия пользователя")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле \"{0}\" обязательное")]
        [Display(Name = "Телефон")]
        [RegularExpression("(((^\\+|^00)\\d{3})( |-|)((\\d{3})( |-|)(\\d{3})( |-|)(\\d{3})))$|^((\\d{3})( |-|)(\\d{3})( |-|)(\\d{3}))$", ErrorMessage = "Неверный формат")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле \"{0}\" обязательное")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Поле \"{0}\" обязательное")]
        [StringLength(100, ErrorMessage = "Значение \"{0}\" должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}