using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using LinkShortener.Backend.Database;
using LinkShortener.Backend.Domain.Entities.Implimentations;
using LinkShortener.Backend.Domain.Repositories.Interfaces;
using LinkShortener.Backend.Domain.Repositories.Implimentations;
using LinkShortener.Backend.Services.Implimentations;
using LinkShortener.Backend.Services.Interfaces;

namespace LinkShortener.Backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }



        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("ConnectionString");
            //services.AddMvc();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LinkShortener.Backend", Version = "v1" });
            });
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddTransient<IShortLinkGenerator, ShortLinkGenerator>();
            services.AddTransient<IBaseRepository<LinkItem>, BaseRepository<LinkItem>>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinkShortener.Backend v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
