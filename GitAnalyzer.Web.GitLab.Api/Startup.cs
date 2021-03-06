#pragma warning disable CS1591
using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using GitAnalyzer.Application.Configuration;
using GitAnalyzer.Application.Services.GitLab;
using GitAnalyzer.Web.Application.MapperProfiles;
using GitAnalyzer.Web.Contracts.GitLab;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GitAnalyzer.Web.GitLab.Api
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Git analyzer GitLab API", Version = "v1" });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(UserMergeRequestsStatisicsContract).Assembly.GetName().Name}.xml"));
            });

            services.AddTransient<IGitLabService, GitLabService>();

            services.AddAutoMapper(typeof(GitLabMapperProfile));
            services.AddCors();

            services.Configure<RepositoriesConfig>(Configuration.GetSection("Repositories"));
            services.Configure<GitLabConfig>(Configuration.GetSection("GitLab"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options =>
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Git analyzer GitLab API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
#pragma warning restore CS1591