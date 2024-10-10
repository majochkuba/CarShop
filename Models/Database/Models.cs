using System;

namespace CarShop.Models.Database;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public byte[] PasswordHash { get; set; }

    public ICollection<Order> Orders { get; set; }
}

public class Car
{
    public int CarId { get; set; }
    public string Make { get; set; } // Marka
    public string Model { get; set; }
    public decimal Price { get; set; }

    public ICollection<Order> Orders { get; set; }
}

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }

    // Relacja do użytkownika
    public int UserId { get; set; }
    public User User { get; set; }

    // Relacja do samochodu
    public int CarId { get; set; }
    public Car Car { get; set; }
}

public class Payment
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    // Relacja do zamówienia
    public int OrderId { get; set; }
    public Order Order { get; set; }
}

