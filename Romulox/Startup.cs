using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Romulox.Core.Entities;
using Romulox.Core.GiantBomb;
using Romulox.Core.Helpers;
using Romulox.Core.Interfaces;
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


            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "client-app/dist";
            });
            
            services.AddDbContext<RomuloxContext>(o => o.UseSqlite("Data Source=romulox.db"));
            services.AddScoped<IPlatformsRepository, PlatformsRepository>();

            var giantBombApiKey = Configuration.GetSection("ApiKeys")["GiantBombApiKey"];
            services.AddScoped<IGameProvider>(g => new GiantBombGameProvider(giantBombApiKey));
            
            var mapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Platform, PlatformDto>();
                    cfg.CreateMap<PlatformCreationDto, Platform>();
                    
                    cfg.CreateMap<Game, GameDto>();
                    cfg.CreateMap<Game, GameUpdateDto>();
                    
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
            
            CreateContentProvider();
            app.UseStaticFiles(new StaticFileOptions()
            {
               ContentTypeProvider = RomFileExtensionProvider.GetRomFileExtensionProvider()
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

        private void CreateContentProvider()
        {
            var platformsDirectory = new DirectoryInfo("wwwroot/Platforms");
            var directories = platformsDirectory.GetDirectories("*.*", System.IO.SearchOption.AllDirectories);;

            foreach (var directory in directories)
            {
                var files = Directory.GetFiles(directory.FullName);
                
                //foreach(var )
                
            }
            
        }
    }
}