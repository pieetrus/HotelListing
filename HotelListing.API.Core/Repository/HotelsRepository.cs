using AutoMapper;
using HotelListing.API.CoreContracts;
using HotelListing.API.Data;

namespace HotelListing.API.CoreRepository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        public HotelsRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
