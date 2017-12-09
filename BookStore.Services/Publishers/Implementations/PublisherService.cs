using BookStore.Data;
using BookStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services.Publishers.Implementations
{
    public class PublisherService : IPublisherService
    {
        private readonly BookStoreDbContext db;

        public PublisherService(BookStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<int> CreateAsync(string publisherName)
        {
            var publisherNameTrimmed = publisherName.Trim();

            if (!await this.ExistsAsync(publisherNameTrimmed))
            {
                var newPublisher = new Publisher
                {
                    Name = publisherNameTrimmed
                };

                this.db.Add(newPublisher);
                await this.db.SaveChangesAsync();

                return newPublisher.Id;
            }
            else
            {
                var publisherId = await this.db
                    .Publishers
                    .Where(a => a.Name == publisherNameTrimmed)
                    .Select(a => a.Id)
                    .FirstOrDefaultAsync();

                return publisherId;
            }
        }

        private async Task<bool> ExistsAsync(string publisherName)
            => await this.db
                .Publishers
                .AnyAsync(a => a.Name.ToLower() == publisherName.ToLower());
    }
}
