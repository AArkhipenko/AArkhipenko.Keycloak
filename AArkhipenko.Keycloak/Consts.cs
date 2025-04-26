using AArkhipenko.Keycloak.Models;

namespace AArkhipenko.Keycloak
{
    /// <summary>
    /// Константы
    /// </summary>
    internal class Consts
    {
        /// <summary>
        /// Раздел в настройках, в котором хранятся все настройки для работы с keycloak
        /// </summary>
        public const string KeyCloakConfigSection = "KeycloakSettings";

        public class Token
        {
            /// <summary>
            /// Название раздела с ИД токена
            /// </summary>
            public const string Id = "jti";

            /// <summary>
            /// Название раздела с издателем токена
            /// </summary>
            public const string Issuer = "iss";

            /// <summary>
            /// Название раздела со списком потребителей токена
            /// </summary>
            public const string Audiences = "aud";

            /// <summary>
            /// Название раздела с ИД пользователя в системе, которая выпускает токен
            /// </summary>
            public const string UserId = "sub";

            /// <summary>
            /// Название раздела с переченем систем, к которым пользователь имеет доступ
            /// </summary>
            public const string ResourceAccess = "resource_access";

            /// <summary>
            /// Название раздела с имем пользователя
            /// </summary>
            public const string Name = "given_name";

            /// <summary>
            /// Название раздела с фамилией пользователя
            /// </summary>
            public const string SurName = "family_name";

            /// <summary>
            /// Название раздела с почтой пользователя
            /// </summary>
            public const string Email = "email";
        }
    }
}
