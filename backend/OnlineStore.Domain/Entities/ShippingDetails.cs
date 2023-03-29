using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Domain.Entities;

public class ShippingDetails
{
    [Required(ErrorMessage = "Введите своё имя")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Введите свой адрес доставки")]
    [Display(Name="Aдрес")]
    public string Adress { get; set; }

    [Required(ErrorMessage = "Укажите город")]
    [Display(Name = "Город")]
    public string City { get; set; }
}