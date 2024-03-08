using AntDesign;
using AutoMapper;
using Blazored.SessionStorage;
using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.Service;
using DoAnTotNghiep.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep
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
            services.AddAntDesign();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddAutoMapperConfig();
            services.AddSingleton<UsersService>();
            services.AddSingleton<BonusDisciplineService>();
            services.AddSingleton<TimekeepingTypeService>();
            services.AddSingleton<OvertimeRateService>();
            services.AddSingleton<TimekeepingFormulaService>();
            services.AddSingleton<TimekeepingShiftService>();
            services.AddSingleton<VacationService>();
            services.AddSingleton<OvertimeService>();
            services.AddSingleton<TimekeepingExplanationService>();
            services.AddSingleton<WorkShiftTableService>();
            services.AddSingleton<FingerprintManagementService>();
            services.AddSingleton<OvertimeAggregateService>();
            services.AddSingleton<TimekeepingAggregateService>();
            services.AddSingleton<SummaryOfTimekeepingService>();
            services.AddSingleton<ISessionFactory>(NHibernateConfig.BuildSessionFactory());
            services.AddBlazoredSessionStorage();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped<ISelectItem, SelectItem>();
            services.AddScoped<NotificationService>();
            services.AddHttpContextAccessor();
            services.AddSingleton<PermissionClaim>();
            services.AddScoped<CustomNotificationManager>();
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
