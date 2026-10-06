using Ecommerce.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);      // Ngăn chặn xóa danh mục nếu có sản phẩm liên quan

            builder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);      // Xóa các hình ảnh liên quan khi xóa sản phẩm

            builder.Entity<Cart>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Cart>()
                .HasIndex(c => c.UserId)
                .IsUnique();

            builder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)                  // Thiết lập quan hệ giữa CartItem và Cart
                .WithMany(c => c.Items)                 // Một Cart có nhiều CartItem
                .HasForeignKey(ci => ci.CartId)         // Khóa ngoại trong CartItem trỏ đến Cart
                .OnDelete(DeleteBehavior.Cascade);      // Xóa các CartItem liên quan khi xóa Cart

            builder.Entity<CartItem>()
                .HasOne(ci => ci.Product)               // Thiết lập quan hệ giữa CartItem và Product
                .WithMany()                             // Một Product có thể có nhiều CartItem
                .HasForeignKey(ci => ci.ProductId)      // Khóa ngoại trong CartItem trỏ đến Product
                .OnDelete(DeleteBehavior.Restrict);     // Ngăn chặn xóa sản phẩm nếu có CartItem liên quan  

            builder.Entity<CartItem>()
                .HasIndex(ci => new { ci.CartId, ci.ProductId })
                .IsUnique();

            builder.Entity<CartItem>()
                .Property(ci => ci.UnitPrice)
                .HasPrecision(18, 2);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            builder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2);

            builder.Entity<OrderItem>()
                .Property(oi => oi.Subtotal)
                .HasPrecision(18, 2);

            builder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany()
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Payment>()
                .Property(p => p.Amount)    
                .HasPrecision(18, 2);
        }
    }   
}
