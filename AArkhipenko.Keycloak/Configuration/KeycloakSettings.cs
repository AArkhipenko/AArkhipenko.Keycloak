using System.ComponentModel.DataAnnotations;

namespace AArkhipenko.Keycloak.Configuration
{
    /// <summary>
	/// Найстройки для конфигурирования работы с keycloak
	/// </summary>
	internal class KeycloakSettings
    {
        /// <summary>
        /// Адрес авторизации
        /// </summary>
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public required string Authority { get; set; }

        /// <summary>
        /// Адрес валидации токена
        /// </summary>
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public required string MetadataAddress { get; set; }

        /// <summary>
        /// Признак необходимости валидации https запросов
        /// </summary>
        public bool IsRequireHttpsMetadata { get; set; } = false;

        /// <summary>
        /// Признак необходимости проверки издателя токена
        /// </summary>
        public bool IsValidateIssuer { get; set; } = false;

        /// <summary>
        /// Список допустимых издателей токенов
        /// </summary>
        public IEnumerable<string> ValidIssuers { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// Признак необходимости проверки потребителя токена
        /// </summary>
        public bool IsValidateAudience { get; set; } = false;

        /// <summary>
        /// Список допустимых потребителей токена
        /// </summary>
        public IEnumerable<string> ValidAudiences { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// <inheritdoc cref="ServiceObject" path="/summary"/>
        /// </summary>
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public required ServiceObject ServiceSettings { get; set; }
    }
}
