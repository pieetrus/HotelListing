using HotelListing.API.CoreModels.Hotel;

namespace HotelListing.API.CoreModels.Country
{
    public class CountryDto : BaseCountryDto
    {
        public int Id { get; set; }
        public List<HotelDto> Hotels { get; set; }
    }
}
