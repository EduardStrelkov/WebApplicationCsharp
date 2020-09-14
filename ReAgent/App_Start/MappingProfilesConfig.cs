using AutoMapper;

namespace ReAgent.App_Start
{
    public class MappingProfilesConfig
    {
        private static IMapper mapper;

        public static IMapper Mapper
        {
            get
            {
                return MappingProfilesConfig.mapper;
            }
        }

        public static void RegisterMapping()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<Mapping.BO.AccountMappingProfile>();
                cfg.AddProfile<Mapping.BO.OrderMappingProfile>();
                cfg.AddProfile<Mapping.View.AccountMappingProfile>();
                cfg.AddProfile<Mapping.View.OrderMappingProfile>();
            });

            MappingProfilesConfig.mapper = configuration.CreateMapper();

            configuration.AssertConfigurationIsValid();
        }
    }
}