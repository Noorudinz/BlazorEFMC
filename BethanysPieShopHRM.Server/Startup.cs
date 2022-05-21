using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BethanysPieShopHRM.Server.Services;
using Microsoft.AspNetCore.Identity;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Syncfusion.Blazor;
using BethanysPieShopHRM.Server.Repository;
using BethanysPieShopHRM.Server.Implementation;

namespace BethanysPieShopHRM.Server
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
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            services.AddScoped<IAuthService, AuthService>();     
            services.AddSyncfusionBlazor();

            //services.AddScoped<HttpClient>(s =>
            //{
            //    var client = new HttpClient { BaseAddress = new System.Uri("https://localhost:44340/") };
            //    //var client = new HttpClient();
            //    //var client = new HttpClient { BaseAddress = new System.Uri("https://bethanyspieshophrmapi.azurewebsites.net/") }; 
            //    return client;
            //});

            //services.AddScoped<IEmployeeDataService, MockEmployeeDataService>();
            services.AddHttpClient<IPayment, PaymentRepository>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
            });
            services.AddHttpClient<IInvoice, InvoiceRepository>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
            });
            services.AddHttpClient<IImports, ImportRepository>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
            });
            services.AddHttpClient<IEmailSetting, EmailRepository>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
            });
            services.AddHttpClient<IPriceFactor, PriceFactorRepository>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
            });
            services.AddHttpClient<IBuilding, BuildingRepository>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
            });
            services.AddHttpClient<IFlatOwner, FlatOwnerRepository>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
            });
            services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
            });
            services.AddHttpClient<ICountryDataService, CountryDataService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
            });
            services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
            });
            services.AddHttpClient<ICompanyDataService, CompanyDataService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
            });
            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
            });
            services.AddHttpClient<IRolesDataService, RolesDataService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
            });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDczMjM5QDMxMzkyZTMyMmUzMEpUZ2s4RWEwbndiaUl1aU4rUW40NEU5YTBveHBaZ2FIUGhaN0VUU3k0RkU9");
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

            app.UseCors(x => x.AllowAnyMethod()
                  .AllowAnyHeader()
                  .SetIsOriginAllowed(origin => true) // allow any origin
                  .AllowCredentials());

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
