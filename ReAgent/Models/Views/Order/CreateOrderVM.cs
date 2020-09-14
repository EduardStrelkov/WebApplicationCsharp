using System.ComponentModel.DataAnnotations;

namespace ReAgent.Models.Views.Order
{
    public class CreateOrderVM
    {
        [MaxLength(500)]
        [MinLength(1)]
        [Required(ErrorMessage = "Поле \"{0}\" обязательное")]
        [Display(Name = "Описание заказа")]
        public string Text { get; set; }
    }
}
