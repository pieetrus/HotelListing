using AutoMapper;
using HotelListing.API.CoreContracts;
using HotelListing.API.Data;
using Microsoft.Extensions.Caching.Distributed;

namespace HotelListing.API.CoreRepository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        public HotelsRepository(HotelListingDbContext context, IMapper mapper, IDistributedCache cache) : base(context, mapper, cache)
        {
        }
    }
}
