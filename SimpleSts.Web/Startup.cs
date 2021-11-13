﻿using ids4.sts.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using IdentityServer4.Services;
using ids4.sts.Extensions;
using System.Collections.Generic;
using ids4.sts.Services;
using ids4.sts.Contracts;
using ids4.sts;
using ids4.sts.Core.Data;
using System.Reflection;
using Microsoft.Extensions.Options;

namespace SimpleSts.Web
{
    public partial class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            var providers = new List<ExternalProvider>();
            Configuration.Bind("ExternalProviders", providers);

            services.AddSingleton(x => new Providers { ExternalProviders = providers });
            services.AddSingleton<IProfileService, ProfileService>();

            services.AddControllersWithViews();

            // Adding DatabaseContext for use in application
            //services.AddDbContext<ApplicationDbContext>(options =>
            //   options.UseSqlServer(connectionString));

            services.AddSqlServer<ApplicationDbContext>(connectionString);

            services.AddIdentityWithEntityFrameworkStore();

            services.AddIdentityServerWithDatabase((dbContextBuilderOptions) =>
            {
                dbContextBuilderOptions.UseSqlServer(connectionString, sql => sql.MigrationsAssembly("SimpleSts.Core"));
            });

            services.AddExternalProviders(providers);
        }

        public void Configure(IApplicationBuilder app)
        {
            Seeder.InitializeDatabase(app);

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add MVC
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();

            // uncomment, if you want to add MVC
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
