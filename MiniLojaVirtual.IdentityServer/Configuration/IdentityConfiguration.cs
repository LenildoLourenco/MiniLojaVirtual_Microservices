using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace MiniLojaVirtual.IdentityServer.Configuration;

public class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Cliente";

    // Permite modelar um escopo que permitirá que um aplicativo cliente
    // exiba um subconjunto de declarações sobre um usuário.
    // Ex:O escopo profile permite que o aplicativo veja declarações
    // sobre o usuário, como nome e data de nascimento
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

    //Representam o que um aplicativo cliente tem permissão para fazer ou acessar
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            // MiniLoja é aplicação web que vai acessar
            // o IdentityServer para obter o token

            new ApiScope("MiniLoja", "MiniLoja Server"),
            new ApiScope(name: "read", "Read data."),
            new ApiScope(name: "write", "Write data."),
            new ApiScope(name: "delete", "Delete data.")
        };

    // Lista de Clientes(aplicativos) que podem usar seu sistema;
    // Cada aplicativo cliente é configurado para ter permissão apenas para fazer certas coisas
    // Nossos clientes (minilojavirtual.web) vão solitar um token ao IdentityServer
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
               //cliente genérico
                new Client
                {
                    ClientId = "cliente",
                    ClientSecrets = { new Secret("abracadabra#simsalabim".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials, //precisa das credenciais do usuário
                    AllowedScopes = {"read", "write", "profile" }
                },
                new Client
                {
                    ClientId = "miniloja",
                    ClientSecrets = { new Secret("abracadabra#simsalabim".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code, //via codigo
                    RedirectUris = {"https://localhost:7294/signin-oidc"},//login
                    PostLogoutRedirectUris = {"https://localhost:7294/signout-callback-oidc"},//logout
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "miniloja"
                    }
                }
        };
}
