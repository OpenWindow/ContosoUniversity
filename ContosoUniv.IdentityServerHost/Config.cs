using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace ContosoUniv.IdentityServerHost
{
  public class Config
  {

    private const string ContosoUniv_API = "api";

    // scopes define the resources in your system that you want to protect, e.g. APIs
    // We will define multiple resources here but give access to particular resources
    // when clients are defined in GetClients() method.
    public static IEnumerable<ApiResource> ApiResources() =>
      new List<ApiResource>
      {
        new ApiResource(ContosoUniv_API, "Contoso Univ API")
      };

    // Defining a client that can access this API 
    // The client will not have an interactive user, and will authenticate using the
    // client secret with Identity server.
    public static IEnumerable<Client> GetClients() =>
      new List<Client>
      {
        new Client
        {
          ClientId = "ContosoUniv.MvcClient",
          // no interactive user, use the clientid/secret for authentication
          AllowedGrantTypes = GrantTypes.ClientCredentials,
          ClientSecrets = { new Secret("secret".Sha256())},
          // scopes(Api Resource) defined in ApiResources method. 
          // Client has access to this resource. 
          AllowedScopes = { ContosoUniv_API  }
        }
      };

  }
}