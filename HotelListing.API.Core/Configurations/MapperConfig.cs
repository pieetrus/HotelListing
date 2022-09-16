using AutoMapper;
using HotelListing.API.CoreData;
using HotelListing.API.CoreModels.Country;
using HotelListing.API.CoreModels.Hotel;
using HotelListing.API.CoreModels.Users;

namespace HotelListing.API.CoreConfigurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();

            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            
            CreateMap<ApiUser, ApiUserDto>().ReverseMap();

        }
    }
}
