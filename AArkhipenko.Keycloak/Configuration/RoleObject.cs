using System.ComponentModel.DataAnnotations;

namespace AArkhipenko.Keycloak.Configuration
{
    /// <summary>
	/// Модель роли
	/// </summary>
	internal class RoleObject
    {
        /// <summary>
        /// Ключ роли
        /// </summary>
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public required string Key { get; set; }

        /// <summary>
        /// Значение роли
        /// </summary>
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public required string Value { get; set; }
    }
}
