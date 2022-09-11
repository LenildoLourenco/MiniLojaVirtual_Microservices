using IdentityModel;
using Microsoft.AspNetCore.Identity;
using MiniLojaVirtual.IdentityServer.Configuration;
using MiniLojaVirtual.IdentityServer.Data;
using System.Security.Claims;

namespace MiniLojaVirtual.IdentityServer.SeedDatabase;

public class DatabaseIdentityServerInitializer : IDatabaseSeedInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void InitializeSeedRoles()
    {
        // Se o perfil Admin não existir, então cria o perfil
        if(! _roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
        {
            // Cria o perfil Admin
            IdentityRole roleAdmin = new IdentityRole();
            roleAdmin.Name = IdentityConfiguration.Admin;
            roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
            _roleManager.CreateAsync(roleAdmin).Wait();
        }
        // Se o perfil Cliente não existir, então cria o perfil
        if(! _roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
        {
            // Cria o perfil Cliente
            IdentityRole roleClient = new IdentityRole();
            roleClient.Name = IdentityConfiguration.Client;
            roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
            _roleManager.CreateAsync(roleClient).Wait();
        }
    }

    public void InitializeSeedUsers()
    {
        // Se o usuário Admin não existir, cria o usuário, define a senha e atribui ao perfil
        if(_userManager.FindByEmailAsync("adminteste@gladiador.com").Result == null)
        {
            // Define os dados do usuário Admin
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "adminteste",
                NormalizedUserName = "ADMINTESTE",
                Email = "adminteste@gladiador.com",
                NormalizedEmail = "ADMINTESTE@GLADIADOR.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (11) 1234-5678",
                FirstName = "Admin",
                LastName = "Teste",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Cria o usuário Admin e atribui a senha
            IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Senha#123").Result;
            if(resultAdmin.Succeeded)
            {
                // Inclui o usuário Admin ao perfil Admin
                _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                // Inclui as claims do usuário Admin
                var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
                }).Result;
            }
        }

        // Se o usuário Cliente não existir, cria o usuário, define a senha e atribui ao perfil
        if(_userManager.FindByEmailAsync("clienteteste@gladiador.com").Result == null)
        {
            // Define os dados do usuário Cliente
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "clienteteste",
                NormalizedUserName = "CLIENTETESTE",
                Email = "clienteteste@gladiador.com",
                NormalizedEmail = "CLIENTETESTE@GLADIADOR.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (81) 5555-8888",
                FirstName = "Cliente",
                LastName = "Teste",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Cria o usuário Cliente e atribui a senha
            IdentityResult resultClient = _userManager.CreateAsync(client, "Senha#123").Result;

            // Inclui o usuário Cliente ao perfil Cliente
            if(resultClient.Succeeded)
            {
                _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                // Adiciona as Claims do usuário Cliente
                var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client),
                }).Result;
            }
        }
    }
}
