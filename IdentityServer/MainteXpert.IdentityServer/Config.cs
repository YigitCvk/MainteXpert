using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource("resource_user", "User API"){Scopes={"user_fullpermission"}},
        new ApiResource("resource_activity", "Activity API"){Scopes={"activity_fullpermission"}},
        new ApiResource("resource_errorcard", "ErrorCard API"){Scopes={"errorcard_fullpermission"}},
        new ApiResource("resource_lookup", "Lookup API"){Scopes={"lookup_fullpermission"}},
        new ApiResource("resource_report", "Report API"){Scopes={"report_fullpermission"}},
        new ApiResource("resource_gateway", "Gateway API"){Scopes={"gateway_fullpermission"}},
        new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
    };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.Email(),
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "roles",
                DisplayName = "Roles",
                Description = "Kullanıcı rolleri",
                UserClaims = new []{ "role" }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("errorcard_fullpermission", "ErrorCard API için full erişim"),
            new ApiScope("activity_fullpermission", "Activity API için full erişim"),
            new ApiScope("report_fullpermission", "Report API için full erişim"),
            new ApiScope("user_fullpermission", "User API için full erişim"),
            new ApiScope("lookup_fullpermission", "Lookup API için full erişim"),
            new ApiScope("gateway_fullpermission", "Gateway API için full erişim"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientName = "Asp.Net Core MVC",
                ClientId = "WebMvcClient",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes =
                {
                    "errorcard_fullpermission",
                    "activity_fullpermission",
                    "lookup_fullpermission",
                    "report_fullpermission",
                    "user_fullpermission",
                    "gateway_fullpermission",
                    IdentityServerConstants.LocalApi.ScopeName
                }
            },
            new Client
            {
                ClientName = "Asp.Net Core MVC",
                ClientId = "WebMvcClientForUser",
                AllowOfflineAccess = true,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes =
                {
                    "errorcard_fullpermission",
                    "activity_fullpermission",
                    "lookup_fullpermission",
                    "report_fullpermission",
                    "user_fullpermission",
                    "gateway_fullpermission",
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    IdentityServerConstants.LocalApi.ScopeName,
                    "roles"
                },
                AccessTokenLifetime = 1 * 60 * 60, // 1 saat
                RefreshTokenExpiration = TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                RefreshTokenUsage = TokenUsage.ReUse
            },
            new Client
            {
                ClientName = "Token Exchange Client",
                ClientId = "TokenExchangeClient",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = new[] { "urn:ietf:params:oauth:grant-type:token-exchange" },
                AllowedScopes =
                {
                    "errorcard_fullpermission",
                    "activity_fullpermission",
                    "lookup_fullpermission",
                    "report_fullpermission",
                    "user_fullpermission",
                    "payment_fullpermission",
                    IdentityServerConstants.StandardScopes.OpenId
                }
            },
        };
}
