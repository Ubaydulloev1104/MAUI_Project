﻿namespace Identity_Application.Contracts.Claim.Responses;

#nullable disable
public record UserClaimsResponse
{
    public string Username { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
    public string Slug { get; set; }
}
