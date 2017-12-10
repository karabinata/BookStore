using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace BookStore.Web.Infrastructure.Extensions
{
    public static class FormFileExtentions
    {
        public static async Task<byte[]> ToByteArrayAsync(this IFormFile formFile)
        {
            using(var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
