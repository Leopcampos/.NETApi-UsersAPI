using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersAPI.Domain.Interfaces.Security;
using UsersAPI.Domain.ValueObjects;
using UsersAPI.Infra.Security.Settings;

namespace UsersAPI.Infra.Security.Services;

public class TokenService : ITokenService
{
    private readonly TokenSettings? _tokenSettings;

    public TokenService(IOptions<TokenSettings> tokenSettings)
        => _tokenSettings = tokenSettings.Value;

    public string CreateToken(UserAuthVO userAuth)
    {
        //Definir as CLAIMS que serão gravadas no token
        //CLAIMS -> Identificações para o usuário
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(userAuth)),
            new Claim(ClaimTypes.Role, userAuth.Role),
        };

        //Gerando a assinatura antifalsificação do token
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings?.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //Preenchendo as informações do token
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _tokenSettings?.Issuer,
            audience: _tokenSettings?.Audience,
            claims: claims, //Informações de identificação do usuário
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_tokenSettings?.ExpirationInMinutes)),
            signingCredentials: credentials //Assinatura do token
        );

        //Retornando o token
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(jwtSecurityToken);
    }
}