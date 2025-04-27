using AArkhipenko.Core.Exceptions;
using AArkhipenko.Keycloak.Models;
using Microsoft.AspNetCore.Authorization;

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
				var tokenModel = TokenModel.Map(context.User.Claims);

				if (!tokenModel.Audiences.Contains(policy.ServiceName) ||
                    !tokenModel.Resources.TryGetValue(policy.ServiceName, out var resourceModel))
				{
					throw new AuthorizationException("Нет доступа к сервису");
				}

                if (!resourceModel.Roles.Contains(policy.RoleName))
				{
					throw new AuthorizationException("Пользователь не имеет необходимых ролей");
				}

				context.Succeed(policy);
			}
			catch (AuthorizationException ex)
			{
				var reasone = new AuthorizationFailureReason(this, ex.Message);
				context.Fail();
			}

			return Task.CompletedTask;
		}
	}
}
