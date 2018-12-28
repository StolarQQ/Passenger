using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Extenstions;
using Passenger.Infrastructure.IoC;
using Passenger.Infrastructure.IoC.Modules;
using Passenger.Infrastructure.Mapper;
using Passenger.Infrastructure.Repositories;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.Settings;

namespace Passenger.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthorization(x => x.AddPolicy("admin", p => p.RequireRole("admin")));


            // TODO Test AppSettings Core 2.2
            //var appsttings = Configuration.GetSection("Jwt");
            //services.Configure<JwtSettings>(appsttings);

            // JWT Implementation
            var jwtSettings = Configuration.GetSettings<JwtSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                   .AddJwtBearer(cfg =>
                   {
                       cfg.TokenValidationParameters = new TokenValidationParameters()
                       {
                           // Creator of Token
                           ValidIssuer = jwtSettings.Issuer,
                           // 
                           ValidateAudience = false,
                           // How our Key its make
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                       };
                   });


            var builder = new ContainerBuilder();
            builder.Populate(services);

            //builder.RegisterType<InMemoryDriverRepository>().As<IDriverRepository>();
            //builder.RegisterType<InMemoryUserRepository>().As<IUserRepository>();
            builder.RegisterModule(new ContainerModule(Configuration));

            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();


            // As of Autofac.Extensions.DependencyInjection 4.3.0 the AutofacDependencyResolver
            // implements IDisposable and will be disposed - along with the application container -
            // when the app stops and the WebHost disposes it.
            //
            // Prior to 4.3.0, if you want to dispose of resources that have been resolved in the
            // application container, register for the "ApplicationStopped" event.
            // You can only do this if you have a direct reference to the container,
            // so it won't work with the above ConfigureContainer mechanism.

            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}
