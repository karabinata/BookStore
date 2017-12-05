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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

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

            base.OnModelCreating(builder);
        }
    }
}
