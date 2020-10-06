using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reviews.Data.Contexts;
using Reviews.Data.Services;

namespace Reviews.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpCacheHeaders((expirationOptions) =>
            {
                expirationOptions.MaxAge = 600;
                expirationOptions.CacheLocation = Marvin.Cache.Headers.CacheLocation.Public;
            },
            (validationOptions) =>
            {
                validationOptions.MustRevalidate = false;
            }
            );
            var connectionString = _configuration["ConnectionString:reviewsConnectionString"];
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddDbContext<ReviewsContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            app.UseResponseCaching();
            app.UseHttpCacheHeaders();
            app.UseMvc();
        }
    }
}
