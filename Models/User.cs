using System.ComponentModel.DataAnnotations;

namespace FlowerCommerceAPI.Models
{
   public class User
{
    public int Id { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}

}
