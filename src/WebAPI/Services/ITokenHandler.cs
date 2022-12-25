﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BikeHistory.Models;
using Microsoft.IdentityModel.Tokens;

namespace BikeHistory.Services;

public interface ITokenHandler
{
    string CreateToken(Guid id, string userName);
    Guid GetUserIdFromRequest(HttpContext context);
    string? GetUserNameFromRequest(HttpContext context);
}

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string? GetUserNameFromRequest(HttpContext context)
    {
        var token = _getTokenFromRequest(context);
        return token?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    }

    public Guid GetUserIdFromRequest(HttpContext context)
    {
        var token = _getTokenFromRequest(context);
        var id = token?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        return GuidHelper.GuidOrEmpty(id);
    }

    public string CreateToken(Guid id, string userName)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var token = _createToken(id, userName);
        return jwtHandler.WriteToken(token);
    }

    private JwtSecurityToken? _getTokenFromRequest(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token is null)
        {
            return null;
        }

        var validatedToken = _validateToken(token);
        if (validatedToken is not null)
        {
            return validatedToken;
        }

        return null;
    }

    private JwtSecurityToken _createToken(Guid id, string userName)
    {
        var claimList = new List<Claim>();
        claimList.Add(new Claim(ClaimTypes.Name, userName));
        claimList.Add(new Claim(ClaimTypes.NameIdentifier, id.ToString()));
        var signKey = _configuration["JWT:Secret"] ?? throw new ArgumentNullException();
        var token = new JwtSecurityToken(
            claims: claimList,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey)),
                SecurityAlgorithms.HmacSha256)
        );
        return token;
    }

    private JwtSecurityToken? _validateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var signKey = _configuration["JWT:Secret"] ?? throw new ArgumentNullException();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);
            return (JwtSecurityToken) validatedToken;
        }
        catch (Exception)
        {
            return null;
        }

    }
}