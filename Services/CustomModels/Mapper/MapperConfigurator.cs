namespace Services.CustomModels.MapperSettings
{
    using AutoMapper;
    using Models;
    using Services.Interface;
    using System;

    public class MapperConfigurator
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ICustomModel, Person>();
            CreateMap<RegisterModel, Person>();
            CreateMap<Role, RoleModel>();
            CreateMap<RoleModel, Role>();
            CreateMap<ProductModel, Product>().ForMember(d => d.Name, opt => opt.MapFrom(x => x.Name));
            CreateMap<Product, ProductModel>().ForMember(d => d.Name, opt => opt.MapFrom(x => x.Name)); ;
            
        }
    }
}
