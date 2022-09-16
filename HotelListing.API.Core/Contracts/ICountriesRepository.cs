using HotelListing.API.Data;

namespace HotelListing.API.CoreContracts
{
    public interface ICountriesRepository : IGenericRepository<Country>
    {
        Task<Country> GetDetails(int id);
    }
}
