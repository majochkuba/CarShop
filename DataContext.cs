using System;
using CarShop.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CarShop;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users {get; set;}
    public DbSet<Order> Orders {get; set;}
    public DbSet<Car> Cars {get; set;}
    public DbSet<Payment> Payments {get; set;}
}