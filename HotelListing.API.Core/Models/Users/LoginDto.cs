using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.CoreModels.Users;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]      
    public string Password { get; set; }
}