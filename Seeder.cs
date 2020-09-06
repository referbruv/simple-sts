using System.Linq;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using ids4.sts.Data;
using ids4.sts.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using IdentityModel;
using System.Security.Claims;
using Serilog;

namespace ids4.sts
{
    public class Seeder
    {
        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                context.Database.Migrate();

                context.Clients.RemoveRange(context.Clients.ToList());

                context.SaveChanges();

                AddClients(context);

                AddIdentityResources(context);

                AddApiScopes(context);

                AddApiResources(context);

                var identityContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                identityContext.Database.Migrate();

                AddUsers(serviceScope, identityContext);
            }
        }

        private static void AddApiResources(ConfigurationDbContext context)
        {
            if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.ApiResources)
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }

        private static void AddApiScopes(ConfigurationDbContext context)
        {
            if (!context.ApiScopes.Any())
            {
                foreach (var apiScope in Config.ApiScopes)
                {
                    context.ApiScopes.Add(apiScope.ToEntity());
                }
            }
        }

        private static void AddIdentityResources(ConfigurationDbContext context)
        {
            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }

        private static void AddClients(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }
        }

        private static void AddUsers(IServiceScope sp, ApplicationDbContext identityContext)
        {
            if (!identityContext.Users.Any())
            {
                var userMgr = sp.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var alice = userMgr.FindByNameAsync("alice").Result;

                if (alice == null)
                {
                    alice = new ApplicationUser
                    {
                        UserName = "alice",
                        Email = "AliceSmith@email.com",
                        EmailConfirmed = true,
                    };
                    
                    var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                    
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        }).Result;
                    
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    
                    Log.Debug("alice created");
                }
                else
                {
                    Log.Debug("alice already exists");
                }
            }
        }
    }
}