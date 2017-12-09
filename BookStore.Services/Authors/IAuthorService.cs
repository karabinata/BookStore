using BookStore.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services.Authors
{
    public interface IAuthorService
    {
        Task<IList<int>> CreateAsync(IList<string> authors);
    }
}
