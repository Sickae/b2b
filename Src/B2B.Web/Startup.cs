using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using B2B.Logic.BusinessLogic.User.Query;
using B2B.Logic.Infrastructure;
using B2B.Web.Infrastructure;
using B2B.Web.Infrastructure.ModelBinders;
using B2B.Web.Models.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHibernate;
using WebpackTag;

namespace B2B.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => { options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider()); })
                .AddRazorRuntimeCompilation()
                .AddFluentValidation(ConfigureFluentValidation);

            services.CreateSessionFactory(Configuration);

            services.AddWebServiceCollection();
            services.AddLogicServiceCollection();

            WebServiceSetup.ConfigureAuthentication(services);

            services.AddScoped(x => x.GetServices<ISessionFactory>().First().OpenSession());
            services.AddScoped(x => x.GetServices<ISessionFactory>().First().OpenStatelessSession());

            services.AddWebpackTag();
            services.AddMediatR(typeof(UserQueryHandler).GetTypeInfo().Assembly);
            services.AddAutoMapper(AutoMapperConfig.Configure, AutoMapperConfig.GetAutoMapperProfileAssemblies());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseStatusCodePagesWithReExecute("/Home/ErrorPage/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ConfigureFluentValidation(FluentValidationMvcConfiguration cfg)
        {
            ValidatorOptions.Global.DisplayNameResolver = (type, member, expression) =>
            {
                if (member == null) return null;
                var name = member.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
                return name ?? member.Name;
            };
            cfg.DisableDataAnnotationsValidation = true;
        }
    }
}
