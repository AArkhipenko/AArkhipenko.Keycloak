using AArkhipenko.Core.Exceptions;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AArkhipenko.Keycloak.Models
{
    /// <summary>
    /// Модель токена
    /// </summary>
    internal class TokenModel
    {
        /// <summary>
        /// ИД токена
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Издатель токена
        /// </summary>
        public required string Issuer { get; set; }

        /// <summary>
        /// Список потребителей токена
        /// </summary>
        public IEnumerable<string> Audiences { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// ИД пользователя в системе, которая выпускает токен
        /// </summary>
        public required string UserId { get; set; }

        /// <summary>
        /// Перечень систем, к которым пользователь имеет доступ
        /// </summary>
        public IDictionary<string, ResourceModel> Resources { get; set; } = new Dictionary<string, ResourceModel>();

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public required string SurName { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Парсинг в перечная claim в модель
        /// </summary>
        /// <param name="claims">список claim</param>
        /// <returns></returns>
        /// <exception cref="AuthorizationException">В токене не задан раздел</exception>
        public static TokenModel Map(IEnumerable<Claim> claims)
        {
            var keys = new[]
            {
                Consts.Token.Id,
                Consts.Token.Issuer,
                Consts.Token.Audiences,
                Consts.Token.UserId,
                Consts.Token.ResourceAccess,
                Consts.Token.Name,
                Consts.Token.SurName,
                Consts.Token.Email
            };

            var errors = keys.Select(x =>
            {
                if (!claims.Any(y => y.Type == x))
                {
                    return x;
                }

                return null;
            }).Where(x => !string.IsNullOrEmpty(x));

            if (errors.Any())
            {
                throw new AuthorizationException($"В токене не заданы разделы: {string.Join(", ", errors)}");
            }

            return new TokenModel()
            {
                Id = claims.First(x => x.Type == Consts.Token.Id).Value,
                Issuer = claims.First(x => x.Type == Consts.Token.Issuer).Value,
                Audiences = claims.Where(x => x.Type == Consts.Token.Audiences)
                    .Select(x => x.Value),
                UserId = claims.First(x => x.Type == Consts.Token.UserId).Value,
                Resources = JsonConvert.DeserializeObject<Dictionary<string, ResourceModel>>(
                    claims.First(x => x.Type == Consts.Token.ResourceAccess).Value),
                Name = claims.First(x => x.Type == Consts.Token.Name).Value,
                SurName = claims.First(x => x.Type == Consts.Token.SurName).Value,
                Email = claims.First(x => x.Type == Consts.Token.Email).Value,
            };
        }
    }
}
