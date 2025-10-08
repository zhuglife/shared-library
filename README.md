# 📦 Shared Libraries - Microservices Project

## 📋 Índice

- [Visão Geral](#visão-geral)
- [Estrutura](#estrutura)
- [Bibliotecas](#bibliotecas)
  - [Common](#common)
  - [HealthChecks](#healthchecks)
  - [Logging](#logging)
  - [Messaging](#messaging)
  - [Security](#security)
- [Como Usar](#como-usar)
- [Testes](#testes)
- [Convenções](#convenções)

---

## 🎯 Visão Geral

A pasta `shared/` contém bibliotecas compartilhadas reutilizáveis por todos os microserviços do projeto. Essas bibliotecas encapsulam funcionalidades comuns, reduzindo duplicação de código e garantindo consistência.

**Benefícios:**
- ✅ **Reutilização**: Código compartilhado entre serviços
- ✅ **Manutenção**: Alterações centralizadas
- ✅ **Consistência**: Padrões unificados
- ✅ **Testabilidade**: Testes unitários isolados
- ✅ **Versionamento**: Controle de versão independente

---

## 📁 Estrutura

```
src/shared/
├── Common/
│   ├── Exceptions/          # Exceções customizadas
│   ├── Extensions/          # Extension methods
│   ├── Models/             # Modelos compartilhados
│   └── Validators/         # Validadores FluentValidation
│
├── HealthChecks/           # Health checks customizados
│   ├── Checks/            # Implementações de checks
│   └── Extensions/        # Extensions para configuração
│
├── Logging/               # Logging compartilhado
│   ├── Configuration/     # Configurações de log
│   └── Extensions/        # Extensions para logging
│
├── Messaging/            # Mensageria (Kafka)
│   ├── Commands/         # Command messages
│   ├── Events/           # Event messages
│   └── Kafka/           # Implementações Kafka
│       ├── Configuration/
│       └── Services/
│
└── Security/            # Segurança e autenticação
    ├── Extensions/      # Extensions para JWT/Auth
    ├── Models/         # Models de segurança
    └── Services/       # Services de token
```

---

## 📚 Bibliotecas

### 🔷 Common

**Namespace:** `Common.*`  
**Assembly:** `Common.*.dll`

Contém funcionalidades básicas compartilhadas por todos os serviços.

#### 📂 Common.Exceptions

Exceções customizadas para tratamento de erros consistente.

**Classes:**
```csharp
namespace Common.Exceptions;

public class NotFoundException : Exception
public class ValidationException : Exception
public class DomainException : Exception
public class ConflictException : Exception
public class UnauthorizedException : Exception
public class ForbiddenException : Exception
```

**Exemplo de uso:**
```csharp
using Common.Exceptions;

// Lançar exceção
if (order == null)
    throw new NotFoundException($"Order {orderId} not found");

// Validação
if (!order.IsValid())
    throw new ValidationException("Order validation failed", errors);
```

#### 📂 Common.Extensions

Extension methods úteis para tipos comuns.

**Classes:**

**DateTimeExtensions.cs**
```csharp
namespace Common.Extensions;

public static class DateTimeExtensions
{
    public static bool IsWeekend(this DateTime date);
    public static bool IsBusinessDay(this DateTime date);
    public static DateTime StartOfDay(this DateTime date);
    public static DateTime EndOfDay(this DateTime date);
    public static int GetAge(this DateTime birthDate);
}
```

**StringExtensions.cs**
```csharp
public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? value);
    public static bool IsValidEmail(this string email);
    public static string ToSlug(this string text);
    public static string Truncate(this string value, int maxLength);
    public static string? NullIfEmpty(this string? value);
}
```

**EnumerableExtensions.cs**
```csharp
public static class EnumerableExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source);
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source);
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action);
}
```

**Exemplo de uso:**
```csharp
using Common.Extensions;

// DateTime
var isWeekend = DateTime.Now.IsWeekend();
var startOfDay = DateTime.Now.StartOfDay();

// String
var email = "user@example.com";
if (email.IsValidEmail()) { }

var slug = "Hello World!".ToSlug(); // "hello-world"

// Enumerable
var items = new List<string> { "a", null, "b" };
var notNull = items.WhereNotNull();
```

#### 📂 Common.Models

Modelos compartilhados usados em todo o projeto.

**Common.Models.Entities**
```csharp
namespace Common.Models.Entities;

// Entidade base
public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
}

// Entidade com auditoria
public abstract class BaseAuditableEntity : BaseEntity
{
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; protected set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted => DeletedAt.HasValue;
}
```

**Common.Models.Enums**
```csharp
namespace Common.Models.Enums;

public enum EntityStatus
{
    Active = 1,
    Inactive = 2,
    Pending = 3,
    Deleted = 4
}
```

**Common.Models.Pagination**
```csharp
namespace Common.Models.Pagination;

public class PagedRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;
}
```

**Common.Models.Results**
```csharp
namespace Common.Models.Results;

// Result pattern para operações sem retorno
public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }
    public List<string> Errors { get; }
    
    public static Result Success();
    public static Result Failure(string error);
    public static Result Failure(List<string> errors);
}

// Result pattern para operações com retorno
public class Result<T> : Result
{
    public T? Value { get; }
    
    public static Result<T> Success(T value);
    public static new Result<T> Failure(string error);
    public static new Result<T> Failure(List<string> errors);
}
```

**Exemplo de uso:**
```csharp
using Common.Models.Entities;
using Common.Models.Results;
using Common.Models.Pagination;

// Entidade
public class Customer : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
}

// Result pattern
public Result<Customer> GetCustomer(Guid id)
{
    var customer = _repository.Find(id);
    
    if (customer == null)
        return Result<Customer>.Failure("Customer not found");
    
    return Result<Customer>.Success(customer);
}

// Paginação
public async Task<PagedResult<Customer>> GetCustomersAsync(PagedRequest request)
{
    var query = _dbContext.Customers.AsQueryable();
    
    var totalCount = await query.CountAsync();
    var items = await query
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize)
        .ToListAsync();
    
    return new PagedResult<Customer>
    {
        Items = items,
        TotalCount = totalCount,
        PageNumber = request.PageNumber,
        PageSize = request.PageSize
    };
}
```

---

### 🔷 HealthChecks

**Namespace:** `HealthChecks.Common.*`  
**Assembly:** `HealthChecks.Common.dll`

Health checks customizados para monitoramento de serviços.

**Classes:**

**DatabaseHealthCheck.cs**
```csharp
namespace HealthChecks.Common.Checks;

public class DatabaseHealthCheck : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, 
        CancellationToken cancellationToken = default)
    {
        // Verifica conexão com banco
    }
}
```

**KafkaHealthCheck.cs**
```csharp
public class KafkaHealthCheck : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, 
        CancellationToken cancellationToken = default)
    {
        // Verifica conexão com Kafka
    }
}
```

**HealthCheckExtensions.cs**
```csharp
namespace HealthChecks.Common.Extensions;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddCustomHealthChecks(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>("database")
            .AddCheck<KafkaHealthCheck>("kafka")
            .AddRedis(configuration.GetConnectionString("Redis"));
            
        return services;
    }
}
```

**Exemplo de uso:**
```csharp
// Program.cs
builder.Services.AddCustomHealthChecks(builder.Configuration);

app.MapHealthChecks("/health");
app.MapHealthChecks("/health/ready");
app.MapHealthChecks("/health/live");
```

---

### 🔷 Logging

**Namespace:** `Logging.Common.*`  
**Assembly:** `Logging.Common.dll`

Configurações padronizadas de logging.

**LoggingSettings.cs**
```csharp
namespace Logging.Common.Configuration;

public class LoggingSettings
{
    public string MinimumLevel { get; set; } = "Information";
    public bool EnableConsole { get; set; } = true;
    public bool EnableFile { get; set; } = false;
    public bool EnableElasticsearch { get; set; } = false;
    public ElasticsearchSettings? Elasticsearch { get; set; }
}

public class ElasticsearchSettings
{
    public string Url { get; set; } = string.Empty;
    public string IndexFormat { get; set; } = "logs-{0:yyyy.MM}";
}
```

**Exemplo de uso:**
```csharp
// appsettings.json
{
  "Logging": {
    "MinimumLevel": "Information",
    "EnableConsole": true,
    "EnableElasticsearch": true,
    "Elasticsearch": {
      "Url": "http://localhost:9200",
      "IndexFormat": "microservices-logs-{0:yyyy.MM}"
    }
  }
}

// Program.cs
var loggingSettings = builder.Configuration
    .GetSection("Logging")
    .Get<LoggingSettings>();
```

---

### 🔷 Messaging

**Namespace:** `Messaging.*`  
**Assembly:** `Messaging.Events.dll`, `Messaging.Kafka.dll`

Sistema de mensageria com Kafka.

#### 📂 Messaging.Events

**IntegrationEvent.cs** (Base para eventos)
```csharp
namespace Messaging.Events.Base;

public abstract class IntegrationEvent
{
    public Guid Id { get; }
    public DateTime OccurredAt { get; }
    
    protected IntegrationEvent()
    {
        Id = Guid.NewGuid();
        OccurredAt = DateTime.UtcNow;
    }
}
```

**OrderCreatedEvent.cs**
```csharp
namespace Messaging.Events.Order;

public class OrderCreatedEvent : IntegrationEvent
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemDto> Items { get; set; }
}

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
```

#### 📂 Messaging.Kafka

**KafkaSettings.cs**
```csharp
namespace Messaging.Kafka.Configuration;

public class KafkaSettings
{
    public string BootstrapServers { get; set; } = "localhost:9092";
    public string GroupId { get; set; } = string.Empty;
    public bool EnableAutoCommit { get; set; } = true;
    public int AutoCommitIntervalMs { get; set; } = 5000;
}
```

**KafkaProducer.cs**
```csharp
namespace Messaging.Kafka.Services;

public class KafkaProducer : IKafkaProducer
{
    public async Task PublishAsync<T>(
        string topic, 
        T message, 
        CancellationToken cancellationToken = default) 
        where T : IntegrationEvent
    {
        // Publica mensagem no Kafka
    }
}
```

**KafkaConfiguration.cs**
```csharp
namespace Messaging.Kafka.Configuration;

public static class KafkaConfiguration
{
    public static IServiceCollection AddKafkaProducer(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var settings = configuration
            .GetSection("Kafka")
            .Get<KafkaSettings>();
            
        services.AddSingleton<IKafkaProducer, KafkaProducer>();
        
        return services;
    }
}
```

**Exemplo de uso:**
```csharp
// appsettings.json
{
  "Kafka": {
    "BootstrapServers": "localhost:9092",
    "GroupId": "order-service",
    "EnableAutoCommit": true
  }
}

// Program.cs
builder.Services.AddKafkaProducer(builder.Configuration);

// Service
public class OrderService
{
    private readonly IKafkaProducer _producer;
    
    public async Task CreateOrderAsync(CreateOrderCommand command)
    {
        // Criar order...
        
        // Publicar evento
        var @event = new OrderCreatedEvent
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            TotalAmount = order.TotalAmount,
            Items = order.Items.Select(i => new OrderItemDto 
            { 
                ProductId = i.ProductId, 
                Quantity = i.Quantity, 
                Price = i.Price 
            }).ToList()
        };
        
        await _producer.PublishAsync("order.created", @event);
    }
}
```

---

### 🔷 Security

**Namespace:** `Security.Common.*`  
**Assembly:** `Security.Common.dll`

Segurança, autenticação e autorização.

**JwtSettings.cs**
```csharp
namespace Security.Common.Models;

public class JwtSettings
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationMinutes { get; set; } = 60;
    public int RefreshTokenExpirationDays { get; set; } = 7;
}
```

**TokenResponse.cs**
```csharp
public class TokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
```

**UserClaims.cs**
```csharp
public class UserClaims
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
    public Dictionary<string, string> CustomClaims { get; set; } = new();
}
```

**ITokenService.cs** & **TokenService.cs**
```csharp
namespace Security.Common.Services;

public interface ITokenService
{
    TokenResponse GenerateToken(UserClaims claims);
    UserClaims? ValidateToken(string token);
    string GenerateRefreshToken();
}

public class TokenService : ITokenService
{
    // Implementação JWT
}
```

**Exemplo de uso:**
```csharp
// appsettings.json
{
  "Jwt": {
    "Secret": "your-super-secret-key-min-32-characters",
    "Issuer": "MicroservicesProject",
    "Audience": "MicroservicesProject.Api",
    "ExpirationMinutes": 60,
    "RefreshTokenExpirationDays": 7
  }
}

// Program.cs
builder.Services.AddJwtAuthentication(builder.Configuration);

// LoginService
public class LoginService
{
    private readonly ITokenService _tokenService;
    
    public async Task<TokenResponse> LoginAsync(LoginRequest request)
    {
        // Validar usuário...
        
        var claims = new UserClaims
        {
            UserId = user.Id,
            Email = user.Email,
            Roles = user.Roles.ToList()
        };
        
        return _tokenService.GenerateToken(claims);
    }
}
```

---

## 🚀 Como Usar

### 1. Referenciando as Bibliotecas

**No seu .csproj:**
```xml
<ItemGroup>
  <!-- Exceções e modelos comuns -->
  <ProjectReference Include="..\..\shared\Common\Exceptions\Common.Exceptions.csproj" />
  <ProjectReference Include="..\..\shared\Common\Extensions\Common.Extensions.csproj" />
  <ProjectReference Include="..\..\shared\Common\Models\Common.Models.csproj" />
  
  <!-- Health checks -->
  <ProjectReference Include="..\..\shared\HealthChecks\HealthChecks.Common.csproj" />
  
  <!-- Messaging -->
  <ProjectReference Include="..\..\shared\Messaging\Events\Messaging.Events.csproj" />
  <ProjectReference Include="..\..\shared\Messaging\Kafka\Messaging.Kafka.csproj" />
  
  <!-- Security -->
  <ProjectReference Include="..\..\shared\Security\Security.Common.csproj" />
</ItemGroup>
```

### 2. Configuração no Program.cs

```csharp
using Common.Extensions;
using HealthChecks.Common.Extensions;
using Messaging.Kafka.Configuration;
using Security.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços compartilhados
builder.Services.AddCustomHealthChecks(builder.Configuration);
builder.Services.AddKafkaProducer(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configurar middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health");

app.Run();
```

### 3. Usando nas Classes

```csharp
using Common.Exceptions;
using Common.Extensions;
using Common.Models.Results;
using Messaging.Events.Order;
using Messaging.Kafka.Services;

public class OrderApplicationService
{
    private readonly IOrderRepository _repository;
    private readonly IKafkaProducer _producer;
    
    public async Task<Result<OrderDto>> CreateOrderAsync(CreateOrderCommand command)
    {
        try
        {
            // Validação
            if (command.Items.IsNullOrEmpty())
                return Result<OrderDto>.Failure("Order must have items");
            
            // Criar order
            var order = Order.Create(command);
            await _repository.AddAsync(order);
            
            // Publicar evento
            var @event = new OrderCreatedEvent
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                TotalAmount = order.TotalAmount
            };
            
            await _producer.PublishAsync("order.created", @event);
            
            return Result<OrderDto>.Success(order.ToDto());
        }
        catch (ValidationException ex)
        {
            return Result<OrderDto>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<OrderDto>.Failure("Failed to create order");
        }
    }
}
```

---

[## 🧪 Testes - DEPRECATED

Cada biblioteca compartilhada tem testes unitários correspondentes:

```
tests/unit/Shared/
├── Common.Exceptions.Tests/
├── Common.Extensions.Tests/
├── Common.Models.Tests/
├── HealthChecks.Common.Tests/
├── Logging.Common.Tests/
├── Messaging.Events.Tests/
└── Security.Common.Tests/
```

**Executar testes:**
```bash
# Todos os testes de shared
dotnet test tests/unit/Shared/

# Testes específicos
dotnet test tests/unit/Shared/Common.Extensions.Tests/
dotnet test tests/unit/Shared/Security.Common.Tests/
```

---
](url)
## 📝 Convenções

### Namespaces

```
Common.*                  # Bibliotecas comuns gerais
HealthChecks.Common.*    # Health checks
Logging.Common.*         # Logging
Messaging.*              # Mensageria
Security.Common.*        # Segurança
```

### Nomenclatura

- **Classes**: `PascalCase`
- **Interfaces**: `IPascalCase`
- **Métodos**: `PascalCase`
- **Parâmetros**: `camelCase`
- **Constantes**: `UPPER_SNAKE_CASE`

### Versionamento

Seguimos [Semantic Versioning](https://semver.org/):
- **MAJOR**: Breaking changes
- **MINOR**: Novas funcionalidades (backward compatible)
- **PATCH**: Bug fixes

### Documentação

- Todas as classes públicas devem ter XML comments
- Todos os métodos públicos devem ter XML comments
- Exemplos de uso devem ser incluídos quando relevante

---

## 🔄 Atualização

Para atualizar referências shared em todos os serviços:

```bash
# Script para atualizar (criar se não existir)
./scripts/update-shared-references.sh
```

---

## 📊 Estatísticas

```
Total de Shared Libraries: 5
Total de Namespaces: 23
Total de Classes: 50+
Cobertura de Testes: 85%+
```

---

## 🤝 Contribuindo

1. Sempre adicione testes unitários
2. Mantenha backward compatibility quando possível
3. Documente com XML comments
4. Atualize este README quando adicionar novas funcionalidades

---

## 📚 Referências

- [Result Pattern](https://enterprisecraftsmanship.com/posts/functional-c-handling-failures-input-errors/)
- [FluentValidation](https://docs.fluentvalidation.net/)
- [Health Checks](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks)
- [Kafka](https://kafka.apache.org/documentation/)
- [JWT Authentication](https://jwt.io/)

---

**Última atualização:** 08/10/2025  
**Mantido por:** DevOps Team
