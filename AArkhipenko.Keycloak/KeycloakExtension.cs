using AArkhipenko.Keycloak.Authority;
using AArkhipenko.Keycloak.Configuration;
using AArkhipenko.Keycloak.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AArkhipenko.Keycloak
{
    /// <summary>
	/// Методы расширешения для работы с keycloak
	/// </summary>
	public static class KeycloakExtension
    {
        /// <summary>
		/// Добавление возможности работы с keycloak
		/// </summary>
		/// <param name="services"><see cref="IServiceCollection"/></param>
		/// <param name="configs"><see cref="IConfiguration"/></param>
		/// <returns><see cref="IServiceCollection"/></returns>
		public static IServiceCollection AddKeycloakAuth(this IServiceCollection services, IConfiguration configs)
        {
            var keycloakSettings = configs.GetRequiredSection(Consts.KeyCloakConfigSection).Get<KeycloakSettings>();
            if (keycloakSettings is null)
            {
                throw new ApplicationException("Не найден раздел с настройками для работы с keycloak");
            }

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.MapInboundClaims = false;
                    options.Authority = keycloakSettings.Authority;
                    options.MetadataAddress = keycloakSettings.MetadataAddress;
                    options.RequireHttpsMetadata = keycloakSettings.IsRequireHttpsMetadata;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = keycloakSettings.IsValidateIssuer,
                        ValidIssuers = keycloakSettings.ValidIssuers,
                        ValidateAudience = keycloakSettings.IsValidateAudience,
                        ValidAudiences = keycloakSettings.ValidAudiences,
                    };
                });

            services.AddAuthorization(options =>
            {
                foreach(var role in keycloakSettings.ServiceSettings.Roles)
                {
                    options.AddPolicy(role.Key, policy =>
                    {
                        policy.AddRequirements(new RoleAccessPolicy(
                            keycloakSettings.ServiceSettings.Service,
                            role.Value));
                    });
                }
            });

            services
                .AddScoped<IAuthorizationHandler, RoleAccessHandler>()
                .AddScoped<IClaimsTransformation, CustomClaimsTransformation>();

            return services;
        }
    }
}
