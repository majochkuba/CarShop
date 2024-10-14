using System;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Models;

public class LoginData
{
    [Required(ErrorMessage = "Nazwa użytkownika jest wymagana.")]
    public string UserName { get; set;}

    [Required(ErrorMessage = "Hasło jest wymagane.")]
    public string Password { get; set; }
    
    
}
