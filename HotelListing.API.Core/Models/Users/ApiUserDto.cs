using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.CoreModels.Users;

public class ApiUserDto
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]      
    public string Password { get; set; }
}