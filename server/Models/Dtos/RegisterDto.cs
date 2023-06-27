using System.ComponentModel.DataAnnotations;

namespace server.Models.Dtos;

public class RegisterDto
{
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string ConfirmPassword { get; set; }
}