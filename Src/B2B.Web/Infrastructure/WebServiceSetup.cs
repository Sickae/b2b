using System;
using B2B.Logic.Identity;
using B2B.Shared.Interfaces;
using B2B.Web.Models;
using B2B.Web.Models.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace B2B.Web.Infrastructure
{
    public static class WebServiceSetup
    {
        public static IServiceCollection AddWebServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<IAppContext, AppContext>();

            // Validators
            services.AddScoped<IValidator<LoginViewModel>, LoginViewModelValidator>();

            return services;
        }

        public static void ConfigureAuthentication(IServiceCollection services)
        {
            // TODO: set properly
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
                opt.Lockout.MaxFailedAccessAttempts = 5;
            });

            services.AddIdentityCore<AppIdentityUser>()
                .AddUserStore<IdentityUserStore>()
                .AddUserManager<IdentityUserManager>()
                .AddSignInManager<SignInManager>()
                .AddErrorDescriber<IdentityErrorDescriber>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            }).AddIdentityCookies();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(365);
                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Home/AccessDenied";
                opt.ReturnUrlParameter = "returnUrl";
                opt.SlidingExpiration = true;
            });

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(3);
            });
        }
    }
}
