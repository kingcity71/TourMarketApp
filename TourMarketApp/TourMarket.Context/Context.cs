using Microsoft.EntityFrameworkCore;
using System;
using TourMarket.Entities;

namespace TourMarket.DAL
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        

        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelPlace> HotelPlaces { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<State> States { get; set; }
    }
}
//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{

//    modelBuilder.Entity<State>().HasData(
//        new State[]
//        {
//            new State() { Id = Guid.NewGuid(), Description = "В корзине" },
//            new State() { Id = Guid.NewGuid(), Description = "Оплачено" }
//        }
//    );
//}
//private void IntitialCreate()
//{
//    con.States.Add(new State() { Id = Guid.NewGuid(), Description = "В корзине" });
//    con.States.Add(new State() { Id = Guid.NewGuid(), Description = "Оплачено" });
//    con.SaveChanges();
//    con.Roles.Add(new Role() { Id = Guid.NewGuid(), Description = "Admin" });
//    con.Roles.Add(new Role() { Id = Guid.NewGuid(), Description = "User" });
//    con.SaveChanges();

//    var canada = new Country()
//    {
//        Id = new Guid(),
//        Name = "Канада"
//    };
//    var usa = new Country()
//    {
//        Id = new Guid(),
//        Name = "США"
//    };

//    con.Countries.Add(canada);

//    con.Countries.Add(usa);

//    con.Cities.Add(new City()
//    {
//        Id = new Guid(),
//        Name = "Оттава",
//        Country = canada
//    });
//    con.Cities.Add(new City()
//    {
//        Id = new Guid(),
//        Name = "Торонто",
//        Country = canada
//    });
//    con.Cities.Add(new City()
//    {
//        Id = new Guid(),
//        Name = "Вашингтон",
//        Country = usa
//    });
//    con.Cities.Add(new City()
//    {
//        Id = new Guid(),
//        Name = "Минисота",
//        Country = usa
//    });
//    con.SaveChanges();

//    var cities = con.Cities.ToArray();
//    var description = "Мини- отель «Винтаж» открыл свои двери в июле 2010 г." +
//       "Отель «Винтаж»-новый мини - отель бизнес класса," +
//        "удачно сочетающий в себе лучшие традиции европейского сервиса и колорит старого Русского города." +
//       "Отель «Винтаж»  уникален по своей архитектуре и расположению в деловом и культурном центре исторической части города," +
//       "в начале пешеходной зоны одной из самых старинных улиц г.Калуги - ул.Театральной," +
//         "являющейся традиционным местом отдыха калужан и гостей города," +
//         "где в изобилии находятся кафе," +
//         "рестораны," +
//         "магазины," +
//         "а по вечерам звучит живая музыка.В нескольких десятках метров от отеля находится символический центр г.Калуги - отметка «Нулевой километр».";
//    var hotel1 = new Hotel()
//    {
//        Id = Guid.NewGuid(),
//        Description = description,
//        Stars = 5,
//        Name = "Vintage",
//        City = cities[0]
//    };
//    var hotel2 = new Hotel()
//    {
//        Id = Guid.NewGuid(),
//        Description = description,
//        Stars = 5,
//        Name = "Capital",
//        City = cities[1]
//    };
//    var hotel3 = new Hotel()
//    {
//        Id = Guid.NewGuid(),
//        Description = description,
//        Stars = 5,
//        Name = "Luxor",
//        City = cities[2]
//    };
//    var hotel4 = new Hotel()
//    {
//        Id = Guid.NewGuid(),
//        Description = description,
//        Stars = 5,
//        Name = "AllStars",
//        City = cities[3]
//    };
//    con.Hotels.Add(hotel1);
//    con.Hotels.Add(hotel2);
//    con.Hotels.Add(hotel3);
//    con.Hotels.Add(hotel4);
//    con.SaveChanges();
//}
//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//{
//    Database.EnsureCreated();
//    //optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=TourMarketDB;");
//}