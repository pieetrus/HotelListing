using HotelListing.API.CoreModels.Country;
using HotelListing.API.Data;

namespace HotelListing.API.CoreContracts
{
    public interface ICountriesRepository : IGenericRepository<Country>
    {
        Task<CountryDto> GetDetails(int id);
    }
}
