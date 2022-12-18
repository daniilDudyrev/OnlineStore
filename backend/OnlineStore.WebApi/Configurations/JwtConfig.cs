using System.Text;

namespace OnlineStore.WebApi.Configurations;

public class JwtConfig
{
    public string SigningKey { get; set; } = "";
    public TimeSpan LifeTime { get; set; }
    public string Audience { get; set; } = "";
    public string Issuer { get; set; } = "";

    public byte[] SigningKeyBytes => Encoding.UTF8.GetBytes(SigningKey);

}