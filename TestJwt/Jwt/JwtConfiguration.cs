using System.Globalization;

namespace JwtAuthApp.JWT;

public class JwtConfiguration
{
    public string Issuer { get; } = string.Empty;
    public string Secret { get;  } = string.Empty;
    public string Audience { get;  } = string.Empty;
    public int ExpireDays { get; } = 0;

    public JwtConfiguration(IConfiguration configuration)
    {
        var section = configuration.GetSection("JWT");

        Issuer = section[nameof(Issuer)] ?? string.Empty;
        Secret = section[nameof(Secret)] ?? string.Empty;
        Audience = section[nameof(Audience)] ?? string.Empty;

        if (!int.TryParse(section[nameof(ExpireDays)], NumberStyles.Integer, CultureInfo.InvariantCulture, out int expireDays))
        {
            expireDays = 7; 
        }

        ExpireDays = expireDays;
    }
}

public class JwtConfigurationFactory(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public JwtConfiguration Create()
    {
        var jwtConfiguration = new JwtConfiguration(_configuration);

        return jwtConfiguration;

    }
}