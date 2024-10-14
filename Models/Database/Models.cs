using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CarShop.Models.Database;

public class User
{
    [ValidateNever]
    public int UserId { get; set; }
    public string UserName { get; set; }
    public byte[] PasswordHash { get; set; }

    [ValidateNever]
    public ICollection<Order> Orders { get; set; }
}

public class Car
{
    [ValidateNever]
    public int CarId { get; set; }
    [Required(ErrorMessage = "Marka jest wymagana.")]
    public string Make { get; set; } // Marka
    [Required(ErrorMessage = "Model jest wymagany.")]
    public string Model { get; set; }
    [Required(ErrorMessage = "Cena jest wymagana.")]
    public decimal Price { get; set; }

    [ValidateNever]
    public ICollection<Order> Orders { get; set; }
}

public class Order
{
    [ValidateNever]
    public int OrderId { get; set; }
    [Required(ErrorMessage = "Data zamówienia jest wymagana.")]
    public DateTime OrderDate { get; set; }

    // Relacja do użytkownika
    [Required(ErrorMessage = "Użytkownik jest wymagany.")]
    [Range(1, 1000000, ErrorMessage = "Użytkownik jest wymagany.")]
    public int UserId { get; set; }

    [ValidateNever]
    public User User { get; set; }

    // Relacja do samochodu
    [Required(ErrorMessage = "Samochód jest wymagany.")]
    [Range(1, 1000000, ErrorMessage = "Samochód jest wymagany.")]
    public int CarId { get; set; }
    
    [ValidateNever]
    public Car Car { get; set; }

    [ValidateNever]
    public bool IsPaymentCompleted { get; set; }
}

public class Payment
{
    [ValidateNever]
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    // Relacja do zamówienia
    public int OrderId { get; set; }
    
    [ValidateNever]
    public Order Order { get; set; }
}

