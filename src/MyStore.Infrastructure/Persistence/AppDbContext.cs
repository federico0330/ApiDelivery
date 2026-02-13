using Microsoft.EntityFrameworkCore;
using MyStore.Domain;

namespace MyStore.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Dish> Dishes {get;set;}
    public DbSet<Category> Categories {get;set;}
    public DbSet<OrderItem> OrderItems {get;set;}
    public DbSet<Status> Statuses {get;set;}
    public DbSet<Order> Orders {get;set;}
    public DbSet<DeliveryType> DeliveryTypes {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(c => c.Name).IsRequired().HasMaxLength(25);
            entity.Property(c => c.Description).IsRequired().HasMaxLength(255);
            entity.Property(c => c.Order);

            entity.HasData(
                new Category
                {
                    Id = 1, Name = "Entradas", Description = "Pequeñas porciones para abrir el apetito antes del plato principal.", Order = 1
                },
                new Category
                {
                    Id = 2, Name = "Ensaladas", Description = "Opciones frescas y livianas, ideales como acompañamiento o plato principal.", Order = 2
                },
                new Category
                {
                    Id = 3, Name = "Minutas", Description = "Platos rápidos y clásicos de bodegón: milanesas, tortillas, revueltos.", Order=3
                },
                new Category
                {
                    Id = 4, Name = "Pastas", Description = "Variedad de pastas caseras y salsas tradicionales.", Order = 5
                },
                new Category
                {
                    Id = 5, Name = "Parrilla", Description = "Cortes de carne asados a la parrilla, servidos con guarniciones.", Order = 4
                },
                new Category
                {
                    Id = 6, Name = "Pizzas", Description = "Pizzas artesanales con masa casera y variedad de ingredientes.", Order=7
                },
                new Category
                {
                    Id = 7, Name = "Sandwiches", Description = "Sandwiches y lomitos completos preparados al momento.", Order=6
                },
                new Category
                {
                    Id = 8, Name = "Bebidas", Description = "Gaseosas, jugos, aguas y opciones sin alcohol.", Order=8
                },
                new Category
                {
                    Id = 9, Name = "Cerveza Artesanal", Description = "Cervezas de producción artesanal, rubias, rojas y negras.", Order=9
                },
                new Category
                {
                    Id = 10, Name="Postres", Description="Clásicos dulces caseros para cerrar la comida.", Order=10
                }
            );
        }
        );

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(d => d.DishId);
            entity.Property(d => d.DishId).IsRequired().ValueGeneratedOnAdd();
            entity.Property(d => d.Name).IsRequired().HasMaxLength(255);
            entity.Property(d => d.Description).IsRequired();
            entity.Property(d => d.Price).IsRequired().HasPrecision(18,2); // 18 dígitos en total, 2 decimales
            entity.Property(d => d.Available);
            entity.Property(d => d.ImageUrl).IsRequired();
            entity.Property(d => d.CreateDate).IsRequired();
            entity.Property(d => d.UpdateDate);
            
            entity.HasOne(d => d.Category)
            .WithMany(c => c.Dishes)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // Evita que se borren platos al eliminar una categoria
        }
        );

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(oi => oi.OrderItemId);
            entity.Property(oi => oi.OrderItemId).IsRequired().ValueGeneratedOnAdd();
            entity.Property(oi => oi.Quantity).IsRequired();
            entity.Property(oi => oi.Notes);
            entity.Property(oi => oi.CreateDate).IsRequired();

            entity.HasOne(oi =>oi.Dish)
            .WithMany(d => d.OrderItems)
            .HasForeignKey(oi => oi.DishId)
            .OnDelete(DeleteBehavior.Restrict);
            //No se eliminará un plato por eliminar un elemento de orden

            entity.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(oi => oi.Status)
            .WithMany(s => s.OrderItems)
            .HasForeignKey(oi => oi.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.OrderId);
            entity.Property(o => o.OrderId).IsRequired().ValueGeneratedOnAdd();
            entity.Property(o => o.DeliveryTo).HasMaxLength(255);
            entity.Property(o => o.Notes);
            entity.Property(o => o.Price).IsRequired().HasPrecision(18,2);
            entity.Property(o => o.CreateDate).IsRequired();
            entity.Property(o => o.UpdateDate);

            entity.HasOne(o => o.DeliveryType)
            .WithMany(dt => dt.Orders)
            .HasForeignKey(o => o.DeliveryTypeId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(o => o.OverallStatus)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(25);

            entity.HasData(
                new Status
                {
                    Id = 1, Name = "Pending"
                },
                new Status
                {
                    Id = 2, Name = "In progress"
                },
                new Status
                {
                    Id = 3, Name = "Ready"
                },
                new Status
                {
                    Id = 4, Name = "Delivery"
                },
                new Status
                {
                    Id = 5, Name = "Closed"
                }
            );
        });

        modelBuilder.Entity<DeliveryType>(entity =>
        {
            entity.HasKey(dt => dt.Id);
            entity.Property(dt => dt.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(dt => dt.Name).IsRequired().HasMaxLength(25);

            entity.HasData(
                new DeliveryType
                {
                    Id = 1, Name = "Delivery"
                },
                new DeliveryType
                {
                    Id = 2, Name = "Take away"
                },
                new DeliveryType
                {
                    Id = 3, Name = "Dine in"
                }
            );
        });
    }
}
