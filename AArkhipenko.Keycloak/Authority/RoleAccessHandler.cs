using AArkhipenko.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AArkhipenko.Keycloak.Authority
{
    /// <summary>
    /// Обработка <see cref="RoleAccessPolicy"/>
    /// </summary>
    public class RoleAccessHandler : AuthorizationHandler<RoleAccessPolicy>
    {
        /// <inheritdoc />
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleAccessPolicy policy)
        {
            try
            {
                if (!context.User.HasClaim(x => x.Type == ClaimTypes.System &&
                    x.Value == policy.ServiceName))
                {
                    throw new AuthorizationException("Пользователь не имеет доступа к системе");
                }

                if (!context.User.IsInRole(policy.RoleName))
                {
                    throw new AuthorizationException("Пользователь не имеет необходимых ролей");
                }

                context.Succeed(policy);
            }
            catch (AuthorizationException ex)
            {
                var reasone = new AuthorizationFailureReason(this, ex.Message);
                context.Fail(reasone);
            }

            return Task.CompletedTask;
        }
    }
}
