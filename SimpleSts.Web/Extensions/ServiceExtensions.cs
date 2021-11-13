using System.Collections.Generic;
using System.Reflection;
using IdentityServer4;
using ids4.sts.Contracts;
using ids4.sts.Data;
using ids4.sts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleSts.Web;

namespace ids4.sts.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddIdentityWithEntityFrameworkStore(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddIdentityServerWithDatabase(this IServiceCollection services, System.Action<DbContextOptionsBuilder> configureDbContext)
        {
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            });
            
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // IdentityResources are Constants Mostly
            // OpenId, Profile, Email and so on
            builder.AddInMemoryIdentityResources(Config.IdentityResources);
            
            // Adds Clients, ApiResources and Cors
            // from Database
            builder.AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = configureDbContext;
            });
            
            // Adds Grants from Database
            builder.AddOperationalStore(options =>
            {
                options.ConfigureDbContext = configureDbContext;
            });

            // Adds IdentityUsers as UserStore
            // from Database
            builder.AddAspNetIdentity<ApplicationUser>();

            // not recommended for production
            // you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            return services;
        }

        public static IServiceCollection AddExternalProviders(this IServiceCollection services, List<ExternalProvider> providers)
        {
            var builder = services.AddAuthentication();
            // Action<OAuthOptions> oauthOptions;
            foreach (var provider in providers)
            {
                // var oauthOptions = new OAuthOptions
                // {
                //     SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                //     ClientId = provider.ClientId,
                //     ClientSecret = provider.ClientSecret
                // };

                switch (provider.AuthenticationScheme.ToLowerInvariant())
                {
                    case "google":
                        builder.AddGoogle(provider.AuthenticationScheme, provider.DisplayName, options => {
                            options.ClientId = provider.ClientId;
                            options.ClientSecret = provider.ClientSecret;
                            options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                        });
                        break;

                    case "facebook":
                        builder.AddFacebook(provider.AuthenticationScheme, provider.DisplayName, options => {
                            options.ClientId = provider.ClientId;
                            options.ClientSecret = provider.ClientSecret;
                            options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                        });
                        break;
                }
            }

            return services;
        }
    }
}