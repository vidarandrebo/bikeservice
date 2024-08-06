using System;
using System.Security.Claims;
using FluentResults;

namespace BikeService.Application.Interfaces;

public interface ITokenHandler
{
    string AccessToken(Guid id, string email);
    string RefreshToken(Guid id, string email);
    Result<ClaimsPrincipal> ValidateToken(string token);
}