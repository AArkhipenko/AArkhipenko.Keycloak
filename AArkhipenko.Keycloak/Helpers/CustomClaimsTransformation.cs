using AArkhipenko.Keycloak.Configuration;
using AArkhipenko.Keycloak.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace AArkhipenko.Keycloak.Helpers
{
    /// <summary>
    /// Прослойка преобразования Claim
    /// </summary>
    internal class CustomClaimsTransformation : IClaimsTransformation
    {
        private readonly KeycloakSettings _settings;

        /// <summary>
		/// Initializes a new instance of the <see cref="CustomClaimsTransformation"/> class
        /// </summary>
        /// <param name="configs"></param>
        public CustomClaimsTransformation(IConfiguration configs)
        {
            this._settings = configs.GetRequiredSection(Consts.KeyCloakConfigSection).Get<KeycloakSettings>() ??
                throw new ApplicationException("Не найден раздел с настройками для работы с keycloak");
        }

        /// <inheritdoc/>
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var tokenModel = TokenModel.Map(principal.Claims);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity();

            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, tokenModel.Id));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.AuthenticationInstant, tokenModel.Issuer));
            claimsIdentity.AddClaim(new Claim(Consts.KeycloakClaim.UserId, tokenModel.UserId));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, tokenModel.Name));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Surname, tokenModel.SurName));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, tokenModel.Email));

            foreach (var item in tokenModel.Audiences)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.System, item));
            }

            if(tokenModel.Resources.ContainsKey(this._settings.ServiceSettings.Service) &&
                tokenModel.Resources.TryGetValue(this._settings.ServiceSettings.Service, out var serviceResource))
            {
                foreach (var item in serviceResource.Roles)
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, item));
                }
            }

            principal.AddIdentity(claimsIdentity);

            return Task.FromResult(principal);
        }
    }
}
