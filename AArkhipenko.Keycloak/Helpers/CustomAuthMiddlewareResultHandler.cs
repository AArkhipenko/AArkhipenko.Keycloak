using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net;
using AArkhipenko.Keycloak.Authority;

namespace AArkhipenko.Keycloak.Helpers
{
    /// <summary>
    /// Обработка результата авторизации
    /// </summary>
    internal class CustomAuthMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler
             DefaultHandler = new AuthorizationMiddlewareResultHandler();

        /// <inheritdoc/>
        public async Task HandleAsync(
            RequestDelegate requestDelegate,
            HttpContext httpContext,
            AuthorizationPolicy authorizationPolicy,
            PolicyAuthorizationResult policyAuthorizationResult)
        {
            // Проверка прав завершилась провалом
            if (!policyAuthorizationResult.Succeeded &&
                CheckCustomPolicy(policyAuthorizationResult))
            {
                var failureReasons = policyAuthorizationResult.AuthorizationFailure?.FailureReasons.Select(x => x.Message) ??
                    new string[] { "Ошибка авторизации" };

                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await httpContext.Response.WriteAsync(string.Join(Environment.NewLine, failureReasons));
                return;
            }

            // Стандартная обработка результата
            await DefaultHandler.HandleAsync(
                requestDelegate,
                httpContext,
                authorizationPolicy,
                policyAuthorizationResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="policyAuthorizationResult"></param>
        /// <returns></returns>
        private bool CheckCustomPolicy(PolicyAuthorizationResult policyAuthorizationResult)
        {
            return policyAuthorizationResult.Forbidden &&
                (policyAuthorizationResult.AuthorizationFailure?.FailCalled ?? false) &&
                (policyAuthorizationResult.AuthorizationFailure?.FailureReasons.All(x => x.Handler.GetType() == typeof(RoleAccessHandler)) ?? false);
        }
    }
}
