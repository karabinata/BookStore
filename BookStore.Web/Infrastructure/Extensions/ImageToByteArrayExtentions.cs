using System.IO;

namespace BookStore.Web.Infrastructure.Extensions
{
    public static class ImageToByteArrayExtentions
    {
        public static byte[] ImageToByteArr(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                using (var memoryStrem = new MemoryStream())
                {
                    streamReader.BaseStream.CopyTo(memoryStrem);

                    return memoryStrem.ToArray();
                }
            }
        }
	}
}
