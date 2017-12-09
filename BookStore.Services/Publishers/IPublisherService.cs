using System.Threading.Tasks;

namespace BookStore.Services.Publishers
{
    public interface IPublisherService
    {
        Task<int> CreateAsync(string publisherName);
    }
}
