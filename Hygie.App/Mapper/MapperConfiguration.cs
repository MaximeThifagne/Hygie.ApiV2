using AutoMapper;

namespace Hygie.App.Mapper
{
    public class MapperConfiguration
    {
        private static readonly Lazy<IMapper> Lazy = new(() =>
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod!.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<HygieMappingProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}
