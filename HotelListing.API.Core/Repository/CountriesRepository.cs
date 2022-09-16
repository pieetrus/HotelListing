﻿using AutoMapper;
using HotelListing.API.CoreContracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.CoreRepository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDbContext _context;

        public CountriesRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

        public async Task<Country> GetDetails(int id)
        {
           return await  _context.Countries.Include(x => x.Hotels).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
