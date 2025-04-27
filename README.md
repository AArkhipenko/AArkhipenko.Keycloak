# AArkhipenko.Keycloak

Nuget-проект с настройками для работы с keycloak

## Методы расширения

Все методы расширения находятся [здесь](./AArkhipenko.Keycloak/KeycloakExtension.cs)

### Настройка для работы с Keycloak

Подключение:
```C#
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Добавление работы с Keycloak
builder.Services.AddKeycloakAuth(builder.Configuration);
...
var app = builder.Build();
app.UseAuthorization();
...
app.MapControllers();

app.Run();
```

## Раздел с настройками

Для конфигурирования программы для работы с keycloak, провайдер настроек должен иметься раздел с настройками следующего вида:
```json
...
"KeycloakSettings": {
	// сервер авторизации (обязательно)
	"Authority": "http://{keycloak_host}/realms/{realm}",
	// адрес валидации токена (обязательно)
	"MetadataAddress": "http://{keycloak_host}/realms/{realm}/.well-known/openid-configuration",
	// Признак необходимости валидации https запросов (по умолчанию false)
	"IsRequireHttpsMetadata": "false",
	// Признак необходимости проверки издателя токена (по умолчанию false)
	"IsValidateIssuer": "true",
	// Список допустимых издателей токенов
	"ValidIssuers": [
		"http://{keycloak_host}/realms/{realm}"
	],
	// Признак необходимости проверки потребителя токена (по умолчанию false)
	"IsValidateAudience": "true",
	// Перечень допустимых потребителей токена
	"ValidAudiences": [
		"{client_for_service}"
	],
	// Настройки конкретного сервиса
	"ServiceSettings": {
		// ИД клиента для сервиса
		"Service": "{client_for_service}",
		// Перечень ролей, которые могут контроллироваться в программе
		"Roles": [
			{
				// Ключ роли
				"Key": "AdminRole",
				// Значение роли
				"Value": "{admin_role_for_client}"
			},
			...
		]
	}
},
...
```