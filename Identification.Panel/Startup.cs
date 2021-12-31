using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Identification.Panel.Data;
using MudBlazor.Services;
using Identification.Panel.Services.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System;
using Identification.Panel.Services.Contracts;
using Identification.Panel.Services;
using Identification.Panel.Handlers;
using Blazored.LocalStorage;
using Blazored.SessionStorage;

namespace Identification.Panel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMudServices();
            services.AddMudBlazorDialog();

            services.AddOptions();
            services.AddAuthorizationCore();
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            services.AddTransient<ValidateHeaderHandler>();

            services.AddScoped<AuthenticationStateProvider, CustomAuthentificationStateProvider>();

            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();
            services.AddHttpClient<IUserService, UserService>();
            services.AddSingleton<HttpClient>();
            services.AddHttpClient<IClientService, ClientService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44325/");
            });

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
