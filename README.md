# K-Backend Case 2 - Product Management API

## Proje Açıklaması

Bu proje, .NET 9 ile geliştirilmiş bir Product Management API'dir. JWT Authentication, Redis Cache entegrasyonu ve CQRS pattern kullanılarak Onion Architecture prensiplerine uygun şekilde tasarlanmıştır.

## Teknolojiler

- **.NET 9** - ASP.NET Core Web API
- **Entity Framework Core** - PostgreSQL / In-Memory Database
- **PostgreSQL** - Veritabanı
- **Redis** - Distributed Cache
- **MediatR** - CQRS Pattern implementasyonu
- **JWT Authentication** - Kimlik doğrulama
- **Serilog** - Logging
- **Swagger** - API Dokümantasyonu
- **React + TypeScript** - Frontend

## Mimari Yapı (Onion Architecture)

```
src/
├── ProductManagement.Core/           # Domain Layer - Entity'ler ve Interface'ler
├── ProductManagement.Application/    # Application Layer - CQRS Commands/Queries/Handlers
├── ProductManagement.Infrastructure/ # Infrastructure Layer - Repository, Cache, JWT Service
└── ProductManagement.API/           # API Layer - Controllers, Middleware
```

## Kurulum ve Çalıştırma

### Gereksinimler
- .NET 9 SDK
- Node.js (Frontend için)
- PostgreSQL (opsiyonel - varsayılan olarak In-Memory kullanılır)
- Redis (opsiyonel - varsayılan olarak In-Memory Cache kullanılır)

### Backend Çalıştırma

```bash
# Proje dizinine gidin
cd C:\Users\pc\Desktop\K-Backend-Case2

# Backend'i çalıştırın
cd src/ProductManagement.API
dotnet run
```

Backend varsayılan olarak `http://localhost:5000` adresinde çalışır.

### Frontend Çalıştırma

```bash
# Frontend dizinine gidin
cd frontend

# Bağımlılıkları yükleyin
npm install

# Frontend'i başlatın
npm start
```

Frontend varsayılan olarak `http://localhost:3000` adresinde çalışır.

### Her İkisini Birlikte Çalıştırma

Ayrı terminal pencerelerinde:
1. Terminal 1: `cd src/ProductManagement.API && dotnet run`
2. Terminal 2: `cd frontend && npm start`

## Veritabanı Konfigürasyonu

### In-Memory Database (Varsayılan)
`appsettings.json` dosyasında:
```json
{
  "UseInMemoryDatabase": true,
  "UseInMemoryCache": true
}
```

### PostgreSQL Kullanımı
1. PostgreSQL'i kurun ve çalıştırın
2. `appsettings.json` dosyasını güncelleyin:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=productmanagement;Username=postgres;Password=postgres"
  },
  "UseInMemoryDatabase": false
}
```
3. Migration oluşturun ve uygulayın:
```bash
cd src/ProductManagement.API
dotnet ef migrations add InitialCreate --project ../ProductManagement.Infrastructure
dotnet ef database update --project ../ProductManagement.Infrastructure
```

### Redis Kullanımı
1. Redis'i kurun ve çalıştırın
2. `appsettings.json` dosyasını güncelleyin:
```json
{
  "ConnectionStrings": {
    "Redis": "localhost:6379"
  },
  "UseInMemoryCache": false
}
```

## API Endpoints

### Auth Endpoints
| Method | Endpoint | Açıklama |
|--------|----------|----------|
| POST | `/api/auth/register` | Kullanıcı kaydı |
| POST | `/api/auth/login` | Kullanıcı girişi (JWT token döner) |

### Product Endpoints (JWT Required)
| Method | Endpoint | Açıklama |
|--------|----------|----------|
| GET | `/api/products` | Tüm ürünleri listele (Cached) |
| GET | `/api/products/{id}` | Tek ürün getir |
| POST | `/api/products` | Yeni ürün ekle |
| PUT | `/api/products/{id}` | Ürün güncelle |
| DELETE | `/api/products/{id}` | Ürün sil |

## Swagger Dokümantasyonu

API çalışırken `http://localhost:5000/swagger` adresinden Swagger UI'a erişebilirsiniz.

## Örnek İstekler

### Kullanıcı Kaydı
```json
POST /api/auth/register
{
    "email": "test@test.com",
    "password": "Password123!",
    "fullName": "Test User"
}
```

### Giriş
```json
POST /api/auth/login
{
    "email": "test@test.com",
    "password": "Password123!"
}
```

### Ürün Ekleme (JWT Token gerekli)
```json
POST /api/products
Authorization: Bearer {token}
{
    "name": "Ürün Adı",
    "description": "Ürün açıklaması",
    "price": 99.99,
    "stock": 100
}
```

## Özellikler

- ✅ JWT ile kimlik doğrulama
- ✅ PostgreSQL veritabanı desteği
- ✅ Redis Cache entegrasyonu
- ✅ In-Memory fallback (PostgreSQL/Redis olmadan çalışabilir)
- ✅ CQRS Pattern (Command/Query ayrımı)
- ✅ Onion Architecture
- ✅ Global Exception Handling
- ✅ Serilog ile logging
- ✅ Swagger API dokümantasyonu
- ✅ SOLID prensipleri
- ✅ Repository Pattern
- ✅ Unit of Work Pattern
- ✅ Cache Invalidation (Ürün ekleme/güncelleme/silme işlemlerinde)

## Proje Yapısı

### Core Layer
- **Entities**: User, Product
- **Interfaces**: IRepository, IUnitOfWork, IUserRepository
- **Exceptions**: NotFoundException, ValidationException

### Application Layer
- **Commands**: CreateProduct, UpdateProduct, DeleteProduct, Register, Login
- **Queries**: GetAllProducts, GetProductById
- **Handlers**: Command ve Query handler'ları
- **DTOs**: Request/Response modelleri
- **Interfaces**: ICacheService, IJwtService, IPasswordHasher

### Infrastructure Layer
- **Data**: ApplicationDbContext, Entity Configurations
- **Repositories**: Repository, UserRepository, UnitOfWork
- **Services**: JwtService, PasswordHasher
- **Caching**: MemoryCacheService (Redis/InMemory destekli)

### API Layer
- **Controllers**: AuthController, ProductsController
- **Middlewares**: ExceptionHandlingMiddleware
- **Extensions**: ServiceExtensions (JWT Configuration)

## Versiyon Geçmişi

- **v1.0.0** - İlk sürüm
  - Auth servisi (Register/Login)
  - Product CRUD işlemleri
  - JWT Authentication
  - Redis/InMemory Cache
  - PostgreSQL/InMemory Database
  - Swagger dokümantasyonu

## Lisans

MIT License
