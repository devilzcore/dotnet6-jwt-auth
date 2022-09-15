using System.ComponentModel.DataAnnotations;

namespace dotnet6_jwt_auth.Models
{
  public class AuthenticateRequest
  {
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
  }
}