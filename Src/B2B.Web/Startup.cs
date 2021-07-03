using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using B2B.Logic.BusinessLogic.User.Query;
using B2B.Logic.Wireup;
using B2B.Web.Infrastructure.ModelBinders;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => { options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider()); })
                .AddRazorRuntimeCompilation()
                .AddFluentValidation(cfg => ConfigureFluentValidation(cfg));

            services.CreateSessionFactory(Configuration);

            services.AddScoped(x => x.GetServices<ISessionFactory>().First().OpenSession());
            services.AddScoped(x => x.GetServices<ISessionFactory>().First().OpenStatelessSession());

            services.AddWebpackTag();
            services.AddMediatR(typeof(UserQueryHandler));

            // authentication, authorization
            // automapper, validators
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Home/ErrorPage/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication();
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
            cfg.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("hu");
            ValidatorOptions.Global.DisplayNameResolver = (type, member, expression) =>
            {
                if (member != null)
                {
                    var name = member.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
                    return name ?? member.Name;
                }

                return null;
            };
        }
    }
}