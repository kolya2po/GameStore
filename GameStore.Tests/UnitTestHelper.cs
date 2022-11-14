using AutoMapper;
using GameStore.BLL;

namespace GameStore.Tests
{
    public class UnitTestHelper
    {
        public static IMapper CreateMapperFromProfile()
        {
            var autoMapperProfile = new AutoMapperProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(autoMapperProfile));

            return new Mapper(config);
        }
    }
}
