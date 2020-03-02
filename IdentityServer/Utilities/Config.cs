using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer.Utilities
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "rc.scope",
                UserClaims =
                {
                    "rc.grandma" //custom scope of claim
                }
            }
        };

        public static IEnumerable<ApiResource> GetApis() => new List<ApiResource>
        {
            new ApiResource("ApiOne"),
            new ApiResource("ApiTwo")
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client
            {
                ClientId = "client_id",
                ClientSecrets = {new Secret("client_secret".ToSha256())},

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"ApiOne"}
            },
            new Client
            {
                ClientId = "client_id_mvc",
                ClientSecrets = {new Secret("client_secret_mvc".ToSha256())},
                RedirectUris = {"https://localhost:44343/signin-oidc"},

                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes =
                {
                    "ApiOne", 
                    "ApiTwo", 
                    IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                    "rc.grandma"
                },
                RequireConsent = false,
                
                //optional. - put all the claims in the id token
                // AlwaysIncludeUserClaimsInIdToken = true
            }
        };
    }
}