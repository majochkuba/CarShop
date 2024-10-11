using System;
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
    public string Make { get; set; } // Marka
    public string Model { get; set; }
    public decimal Price { get; set; }

    [ValidateNever]
    public ICollection<Order> Orders { get; set; }
}

public class Order
{
    [ValidateNever]
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }

    // Relacja do użytkownika
    public int UserId { get; set; }

    [ValidateNever]
    public User User { get; set; }

    // Relacja do samochodu
    public int CarId { get; set; }
    
    [ValidateNever]
    public Car Car { get; set; }
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

