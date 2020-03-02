﻿using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer.Utilities
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApis() => new List<ApiResource> {new ApiResource("ApiOne")};

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client
            {
                ClientId = "client_id",
                ClientSecrets = {new Secret("client_secret".ToSha256())},

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"ApiOne"}
            }
        };
    }
}