using System.ComponentModel.DataAnnotations;

namespace AArkhipenko.Keycloak.Configuration
{
    /// <summary>
	/// Настройки конкретного сервиса
	/// </summary>
	public class ServiceSettings
    {
        /// <summary>
        /// Наименование сервиса, к которому у пользователя должен быть доступ
        /// </summary>
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public required string Service { get; set; }

        /// <summary>
        /// Перечень ролей, которые могут контроллироваться в программе
        /// </summary>
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public IEnumerable<RoleObject> Roles { get; set; } = Enumerable.Empty<RoleObject>();
    }
}
