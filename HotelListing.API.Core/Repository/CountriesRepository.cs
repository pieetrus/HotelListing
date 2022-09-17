using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.CoreContracts;
using HotelListing.API.CoreExceptiions;
using HotelListing.API.CoreModels.Country;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace HotelListing.API.CoreRepository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;

        public CountriesRepository(HotelListingDbContext context, IMapper mapper, IDistributedCache cache) : base(context, mapper, cache)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CountryDto> GetDetails(int id)
        {
            var country = await _context.Countries.Include(q => q.Hotels)
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (country == null)
            {
                throw new NotFoundException(nameof(GetDetails), id);
            }

            return country;
        }
    }
}
