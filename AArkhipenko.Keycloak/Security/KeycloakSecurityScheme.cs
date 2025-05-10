using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace AArkhipenko.Keycloak.Security
{
    /// <summary>
    /// OpenApi настройки для работы с Keycloak авторизацией
    /// </summary>
    public class KeycloakSecurityScheme : OpenApiSecurityScheme
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFullUserQuery"/> class.
        /// </summary>
        private KeycloakSecurityScheme(): base()
        {
        }

        /// <summary>
        /// Стандартный ключ названия схемы
        /// </summary>
        public static string DefaultKey => "BearerAuth";

        /// <summary>
        /// Стандартный объект OpenApi
        /// </summary>
        public static KeycloakSecurityScheme Default
            => new KeycloakSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header \"Authorization: Bearer {token}\"",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = DefaultKey
                }
            };
    }
}
