using System.ComponentModel.DataAnnotations;

namespace BibleQuiz.Application.Configurations;
public class SecurityConfiguration
{
    public static string Name = "Security";
    [Required]
    public string Secret { get; set; }
    [Required]
    public int ExpiryTimeInMinutes { get; set; }
    [Required]
    public string Issuer { get; set; }
}
