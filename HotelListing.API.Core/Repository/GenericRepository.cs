using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Core.Extensions;
using HotelListing.API.CoreContracts;
using HotelListing.API.CoreExceptiions;
using HotelListing.API.CoreModels;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace HotelListing.API.CoreRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GenericRepository(HotelListingDbContext context, IMapper mapper, IDistributedCache cache)
        {
            _context = context;
            this._mapper = mapper;
            this._cache = cache;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TResult> AddAsync<TSource, TResult>(TSource source)
        {
            var entity = _mapper.Map<T>(source);

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TResult>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);

            if (entity is null)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exist(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters)
        {
            var totalSize = await _context.Set<T>().CountAsync();

            var items = await _context.Set<T>()
                .Skip(queryParameters.StartIndex)
                .Take(queryParameters.PageSize)
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<TResult>
            {
                Items = items,
                PageNumber = queryParameters.PageNumber,
                RecordNumber = queryParameters.PageSize,
                TotalCount = totalSize
            };
        }

        public async Task<List<TResult>> GetAllAsync<TResult>()
        {
            var recordKey = typeof(T).Name + "_" + DateTime.Now.ToString("yyyyMMdd_hhmm");

            var records = await _cache.GetRecordAsync<List<T>>(recordKey);

            if (records is null)
            {
                records = await _context.Set<T>().ToListAsync();
                await _cache.SetRecordAsync(recordKey, records);
            }

            return _mapper.Map<List<TResult>>(records);
        }

        public async Task<T> GetAsync(int? id)
        {
            if (id is null)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<TResult?> GetAsync<TResult>(int? id)
        {
            var result = await _context.Set<T>().FindAsync(id);

            if (result is null)
            {
                throw new NotFoundException(typeof(T).Name, id.HasValue ? id : "No key provided");
            }

            return _mapper.Map<TResult>(result);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync<TSource>(int id, TSource source)
        {
            var entity = await GetAsync(id);

            if (entity is null)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }

            _mapper.Map(entity, source);
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
