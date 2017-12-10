using ImageSharp;
using System.IO;

namespace BookStore.Web.Infrastructure.Extensions
{
    public static class ImageExtentions
    {
        public static Image CreateImage(this byte[] image)
        {
                return new Image(image);
        }
    }
}
