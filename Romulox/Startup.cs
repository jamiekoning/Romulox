using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Romulox.Core.Configuration.RomuloxSettings;
using Romulox.Core.Entities;
using Romulox.Core.Helpers;
using Romulox.Core.Models;
using Romulox.DataAccess;
using VueCliMiddleware;

namespace Romulox
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.Configure<RomuloxSettings>(Configuration.GetSection("RomuloxSettings"));

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "client-app/dist";
            });

            services.AddDbContext<RomuloxContext>(o => o.UseSqlite("Data Source=romulox.db"));
            services.AddScoped<IPlatformsRepository, PlatformsRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Platform, PlatformDto>();
                    cfg.CreateMap<PlatformCreationDto, Platform>();
                    
                    cfg.CreateMap<Game, GameDto>();

                    cfg.CreateMap<GameCreationDto, Game>()
                        .ForAllMembers(o => o.Condition((source, destination, sourceProperty) => sourceProperty != null));
                }
            );

            services.AddTransient<IMapper>(m => new Mapper(mapperConfiguration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseStaticFiles(new StaticFileOptions()
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/x-msdownload"
            });
            
            app.UseCookiePolicy();

            app.UseSpaStaticFiles();

            app.UseMvc();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app/";
#if DEBUG
                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve", port: 8080); // optional port
                }
#endif
            });
        }
    }
}