using AutoMapper;
using BookStore.Web.Infrastructure.Mapping;

namespace BookStore.Tests
{
    public class TestStartUp
    {
        private static bool testsInitialized = false;
        
        public static void Initialize()
        {
            if (!testsInitialized)
            {
                Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
                testsInitialized = true;
            }
        }
    }
}
