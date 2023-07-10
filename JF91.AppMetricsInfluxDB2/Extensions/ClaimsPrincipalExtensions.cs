﻿using System.Security.Claims;

namespace JF91.AppMetricsInfluxDB2.Extensions;

public static class ClaimsPrincipalExtensions
{
    // User Name
    public static string? GetName
    (
        this ClaimsPrincipal user
    )
        => user.FindFirst(ClaimTypes.Name)?.Value;
    
    // User Email
    public static string? GetEmail
    (
        this ClaimsPrincipal user
    )
        => user.FindFirst(ClaimTypes.Email)?.Value;
    
    // Username
    public static string? GetUsername
    (
        this ClaimsPrincipal user
    )
        => user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}