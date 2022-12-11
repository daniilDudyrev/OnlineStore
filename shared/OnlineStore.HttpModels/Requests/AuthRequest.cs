using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Requests;

public class AuthRequest
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}