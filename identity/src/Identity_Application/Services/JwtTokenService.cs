﻿using Identity_Application.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity_Application.Services;

internal class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateTokenByClaims(IList<Claim> claims, out DateTime expireDate)
    {
        SymmetricSecurityKey key = new(Encoding.UTF8
            .GetBytes(_configuration["JWT:Secret"]!));

        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);
        expireDate = DateTime.Now.AddDays(int.Parse(_configuration["JWT:RefreshTokenValidityInDays"]!));
        JwtSecurityToken token = new(
            claims: claims,
            expires: expireDate,
            signingCredentials: creds);

        string jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    public string CreateRefreshToken(IList<Claim> claims)
    {
        SymmetricSecurityKey key = new(Encoding.UTF8
            .GetBytes(_configuration["JWT:Secret"]!));

        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

        JwtSecurityToken token = new(
            claims: claims,
            expires: DateTime.Now.AddDays(
                int.Parse(_configuration["JWT:RefreshTokenValidityInDays"]!)),
            signingCredentials: creds);

        string jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
