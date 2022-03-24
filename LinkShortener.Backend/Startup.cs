using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using LinkShortener.Resource.Database;
using LinkShortener.Resource.Domain.Entities.Implimentations;
using LinkShortener.Resource.Domain.Repositories.Interfaces;
using LinkShortener.Resource.Domain.Repositories.Implimentations;
using LinkShortener.Resource.Services.Implimentations;
using LinkShortener.Resource.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using LinkShortener.Auth.Common;

namespace LinkShortener.Resource
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
            services.AddControllers();

            // Setup work with JWT
            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;   // �� ��������������� SSL ��� �������� ������. ��� �������� ������� ����� TRUE 
                        options.TokenValidationParameters = new TokenValidationParameters   // ��������� �������� ������
                        {
                            ValidateIssuer = true,                  // �� ��������� ������� ������
                            ValidIssuer = authOptions.Issuer,       // ����� �������

                            ValidateAudience = true,                // �� ��������� ����������� ������
                            ValidAudience = authOptions.Audience,   // ����� �����������

                            ValidateLifetime = true,                // �� ��������� ����� ���������� ������

                            ValidateIssuerSigningKey = true,        // �� ��������� ���� �������
                            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),   // ������������ ����� �������
                        };
                    });

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            services.AddSingleton<IShortLinkGenerator, ShortLinkGenerator>();
            services.AddTransient<IBaseRepository<LinkItem>, BaseRepository<LinkItem>>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LinkShortener.Backend", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinkShortener.Backend v1"));
            }

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
