using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Data.Models;
using BookStore.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services.Authors.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly BookStoreDbContext db;

        public AuthorService(BookStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<IList<int>> CreateAsync(IList<string> authors)
        {
            var authorIds = new List<int>();

            foreach (var author in authors)
            {
                var authorTrimmed = author.Trim();

                if (!await this.AuthorExists(authorTrimmed))
                {
                    var newAuthor = new Author
                    {
                        Name = authorTrimmed
                    };

                    this.db.Add(newAuthor);
                    await this.db.SaveChangesAsync();

                    authorIds.Add(newAuthor.Id);
                }
                else
                {
                    var authorId = await this.db
                        .Authors
                        .Where(a => a.Name == authorTrimmed)
                        .Select(a => a.Id)
                        .FirstOrDefaultAsync();

                    authorIds.Add(authorId);
                }

            }
            return authorIds;
        }

        private async Task<bool> AuthorExists(string name)
            => await this.db
                .Authors
                .AnyAsync(a => a.Name.ToLower() == name.ToLower());
    }
}
