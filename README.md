# AnimalAllies
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-orange.svg)](https://dotnet.microsoft.com/apps/aspnet)
[![RabbitMQ](https://img.shields.io/badge/RabbitMQ-8.3.6-green.svg)](https://www.rabbitmq.com/)
[![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-9.0.1-purple.svg)](https://docs.microsoft.com/en-us/ef/core/)

AnimalAllies - платформа волонтёрской помощи животным, где оказывается ряд услуг: материальная помощь нуждающимся животным, поиск пропавших питомцев, оставление животного на временное на попечительство в виду отъезда и т.д.

<details><summary><h2>Скриншоты</h2></summary>
Будут добавлены, когда будет готова фронтенд часть платформы
</details>

## Возможности backend`а:
- [x] Аутентификация и авторизация на основе разрешений
- [x] Модуль управления животными и управления видами реализован по ddd
- [x] CRUD операции над всеми сущностями: волонтёр, питомец, порода, вид
- [x] Питомцы обладают позицией, которая отображает их очередность на помощь от волонтера. Позиция может быть изменена в зависимости от условий. 
- [x] Возможность обновления профиля пользователя, в качестве обычного клиента и в роли волонтёра
- [x] Мягкое и полное удаление некоторых сущностей
- [x] Хранение файлов (видео и фото) животных или аваторок пользователей в S3 хранилище, которое реализовано в видео отдельного сервиса
- [x] Манипуляции с S3 хранилищем реализованы через AWS SDK S3 и Minio
- [x] Для хранения файлов предусмотрена мультичастичная загрузка 
- [x] В модуле заявок на волонтёрство реализована следующая бизнес-логика по ddd:
  - [x] Подать заявку на волонтёрство, также проверяется наличие запрета на подачу через доменные события
  - [x] Взять заявку на рассмотрение админом (проверка пользователя закрепляется за ним)
  - [x] Одобрить заявку на волонтёрство, тогда через RabbiMQ будет отправлена команда на создание аккаунта волонтёра в модуле авторизации
  - [x] Отправка заявки админом на доработку с указанием проблемных мест
  - [x] Обновление заявки с последующей отправкой на дальнейшее рассмотрение
  - [x] Отклонить заявку на волонтёрство с указанием причины, после чего, через доменные события создастся запрет на подачу новых волонтёрских заявок сроком на неделю
- [x] В модуле дискуссий реализована следующая бизнес-логика по ddd:
  - [x] Когда админ берёт заявку на рассмотрение в модулей заявок на волонтерство, то в модуле дискуссий через контракты создаётся сущность дискуссии между админом и пользователем для обсуждения вопросов.
  - [x] Реализована функциональность, которая закрывает дискуссию для обоих участников
  - [x] Реализована функциональность, которая позволяет удалять пользователям свои сообщения в дискуссии
  - [x] Реализована функциональность, которая позволяет отправлять сообщений в дискуссии
  - [x] Реализована функциональность, которая позволяет обновлять пользователям свои сообщения в дискуссии
- [x] Реализованы разного вида запросы с пагинацией, фильтрацией и сортировкой. Для повышения производительности был использован CQRS. Для команд - ef core, для запросов - dapper 
- [x] Написаны юнит-тесты для всей бизнес-логики 
- [ ] Добавленно кэширование через Redis для повышения производительности запрос и улучшения пользовательского опыта
- [ ] Проект переведен на .NET 9
- [ ] Добавлены метрики через ElasticSearch, Kibana, Grafana
- [ ] Настроен CI/CD
- [ ] Написаны интеграционные тесты для все фич
- [ ] Реализован файловый сервис
- [ ] Реализован сервис уведомлений
- [ ] Реализован телеграм-бот
- [ ] Реализован сервис поиска пропавших животных через ML.NET

## Возможности Файлового сервиса:
- [x] Скачивание файлов через presigned url
- [x] Загрузка файлов через presigned url
- [x] Удаление файлов через presigned url
- [x] Получения мета-данных файлов из MongoDB
- [x] Мультичастичная загрузка для больших файлов
- [x] Проверка консистентности данных через Hangfire (запланированная задача смотри, сопоставляется ли записи из MongoDB наличие файла в S3 Minio)
- [ ] Реализован слой Communication для связи с другими сервисами
- [ ] Слой Communication добавлен в nu-get    


## Стек:

Вот список наиболее значимых фреймворков из предоставленного списка в формате markdown:

| Фреймворк | Версия | Источник |
| --- | --- | --- |
| AWSSDK.S3 | 3.7.414.1 | nuget.org |
| Hangfire | 1.8.15 | nuget.org |
| Hangfire.Core | 1.8.17 | nuget.org |
| Hangfire.PostgreSql | 1.20.10 | nuget.org |
| Microsoft.AspNetCore.OpenApi | 9.0.1 | nuget.org |
| Microsoft.Extensions.Configuration | 9.0.1 | nuget.org |
| Microsoft.Extensions.Logging.Abstractions | 9.0.1 | nuget.org |
| Minio | 6.0.4 | nuget.org |
| MongoDB.Bson | 3.1.0 | nuget.org |
| MongoDB.Driver | 3.1.0 | nuget.org |
| MongoDB.Driver.Core | 2.30.0 | nuget.org |
| Serilog.AspNetCore | 9.0.0 | nuget.org |
| Serilog.Enrichers.Environment | 3.0.1 | nuget.org |
| Serilog.Enrichers.Thread | 4.0.0 | nuget.org |
| Serilog.Exceptions | 8.4.0 | nuget.org |
| Serilog.Sinks.Seq | 9.0.0 | nuget.org |
| Swashbuckle.AspNetCore | 7.2.0 | nuget.org |
| Swashbuckle.AspNetCore.Swagger | 7.2.0 | nuget.org |
| Swashbuckle.AspNetCore.SwaggerGen | 7.2.0 | nuget.org |
| Swashbuckle.AspNetCore.SwaggerUI | 7.2.0 | nuget.org |
| Mass Transit.RabbitMQ | 8.3.6 | nuget.org |
| MediatR | 12.4.1 | nuget.org |
| Dapper | 2.1.66 | nuget.org |
| Microsoft.EntityFrameworkCore | 9.0.1 | nuget.org |
| Microsoft.AspNetCore | 9.0.1 | nuget.org |
| Npgsql | 9.0.2 | nuget.org |
| Npgsql.EntityFrameworkCore.PostgreSQL | 9.0.3 | nuget.org |
| Scrutor | 6.0.1 | nuget.org |
| FluentValidation | 11.11.0 | nuget.org |
| FluentValidation.DependencyInjectionExtensions | 11.11.0 | nuget.org |
| FluentAssertions | 8.0.1 | nuget.org |
| xunit | 2.5.3; 2.9.0; 2.9.3 | nuget.org |
| xunit.runner.visualstudio | 2.8.2; 3.0.2 | nuget.org |
| Microsoft.Extensions.Logging | 9.0.1 | nuget.org |
| Moq | 4.20.72 | nuget.org |
| Serilog.AspNetCore | 9.0.0 | nuget.org |

## Установка и запуск

### Посредством Docker

`в процессе написания`

### Без использования Docker

`в процессе написания`

## Конфигурация

`в процессе написания`
