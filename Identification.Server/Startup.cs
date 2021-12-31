using MatBlazor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Identification.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using MudBlazor;
using Identification.Server.Services;
using Identification.Server.Validation;
using System;
using System.Net.Http;

namespace Identification.Server
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
            services.AddServerSideBlazor(c => c.DetailedErrors = true);
            services.AddTransient<IValidator<Client>, ClientValidator>();
            services.AddTransient<IValidator<Img>, ImgValidator>();
            services.AddMudServices();
            services.AddMudBlazorDialog();
            services.AddMudBlazorSnackbar();
            services.AddMudBlazorScrollManager();
            services.AddMudBlazorResizeListener();
            services.AddMudBlazorResizeObserver();
            services.AddMudBlazorResizeObserverFactory();
            services.AddMudBlazorScrollListener();
            services.AddMudBlazorJsApi();
            services.AddMatBlazor();
            services.AddSingleton<HttpClient>();
            services.AddHttpClient<IClientService, ClientService>(client =>
            {
                client.BaseAddress = new Uri("http://192.168.2.11:86/");
            });
            services.Configure<ReCAPTCHASettings>(Configuration.GetSection("GooglereCAPTCHA"));
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
            app.UsePathBase("/app/");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
