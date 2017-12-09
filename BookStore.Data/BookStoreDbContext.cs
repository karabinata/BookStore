using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookStore.Data.Models;

namespace BookStore.Data
{
    public class BookStoreDbContext : IdentityDbContext<User>
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BookAuthor> BooksWithAuthors { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderBook> OrderBooks { get; set; }

        public DbSet<Trader> Traders;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            builder
                .Entity<OrderBook>()
                .HasKey(ob => new { ob.OrderId, ob.BookId });

            builder
                .Entity<Book>()
                .HasMany(b => b.Authors)
                .WithOne(a => a.Book)
                .HasForeignKey(a => a.BookId);

            builder
                .Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);

            builder
                .Entity<Publisher>()
                .HasMany(p => p.Books)
                .WithOne(b => b.Publisher)
                .HasForeignKey(b => b.PublisherId);

            builder
                .Entity<Book>()
                .HasMany(b => b.Orders)
                .WithOne(o => o.Book)
                .HasForeignKey(o => o.BookId);

            builder
                .Entity<Order>()
                .HasMany(o => o.Books)
                .WithOne(b => b.Order)
                .HasForeignKey(b => b.OrderId);

            builder
                .Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            base.OnModelCreating(builder);
        }
    }
}
