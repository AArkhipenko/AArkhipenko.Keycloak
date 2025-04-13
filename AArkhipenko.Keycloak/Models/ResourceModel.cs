namespace AArkhipenko.Keycloak.Models
{
    /// <summary>
    /// Модель конфига системы в разделе resource_access
    /// </summary>
    internal class ResourceModel
    {
        /// <summary>
        /// Роли пользователя в рамках системы
        /// </summary>
        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
    }
}
