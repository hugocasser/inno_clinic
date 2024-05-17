using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstractions.Auth;
using Application.Abstractions.Services;
using Application.Options;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Application.Services.Auth;

public class AccessTokensService(IOptions<AccessTokenOptions> options, UserManager<User> userManager,
    IRefreshTokensService refreshTokensService) : IAccessTokensService
{
    public string CreateAccessToken(User user, IList<string> userRoles, CancellationToken cancellationToken)
    {
        if (userRoles is null)
        {
            return string.Empty;
        }
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.Now).ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
        };
        
        if (userRoles is null)
        {
            return string.Empty;
        }
        
        claims.AddRange(userRoles.Select(role => new Claim("role:", role)));
        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(600),
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key)),
                SecurityAlgorithms.HmacSha256Signature));

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
    public Task<IList<string>> GetRolesAsync(User user)
    {
        return userManager.GetRolesAsync(user);
    }
}