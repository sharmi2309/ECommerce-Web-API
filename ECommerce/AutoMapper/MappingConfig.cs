using AutoMapper;
using ECommerce.Models;
using ECommerce.Models.DTO;


namespace ECommerce.AutoMapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Items, ItemsDTO>().ReverseMap();
            CreateMap<Items, ItemCreateDTO>().ReverseMap();
            CreateMap<Items, ItemUpdateDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderCreateDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<User, UserSignupDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();


        }
    }
}
