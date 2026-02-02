# K-Backend Case 2 - Product Management API

## Proje Açıklaması

Bu proje, .NET 9 ile geliştirilmiş bir Product Management API'dir. JWT Authentication, Redis Cache entegrasyonu ve CQRS pattern kullanılarak Onion Architecture prensiplerine uygun şekilde tasarlanmıştır.

## Teknolojiler

- **.NET 9** - ASP.NET Core Web API
- **Entity Framework Core** - In-Memory Database
- **MediatR** - CQRS Pattern implementasyonu
- **JWT Authentication** - Kimlik doğrulama
- **In-Memory Cache** - Redis benzeri cache mekanizması
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

### Backend Çalıştırma

```bash
# Proje dizinine gidin
cd C:\Users\pc\Desktop\K-Backend-Case2

# Backend'i çalıştırın
cd src/ProductManagement.API
dotnet run
```

Backend varsayılan olarak `https://localhost:5001` ve `http://localhost:5000` adreslerinde çalışır.

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

API çalışırken `https://localhost:5001/swagger` adresinden Swagger UI'a erişebilirsiniz.

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
- ✅ In-Memory Cache entegrasyonu (Redis benzeri)
- ✅ CQRS Pattern (Command/Query ayrımı)
- ✅ Onion Architecture
- ✅ Global Exception Handling
- ✅ Serilog ile logging
- ✅ Swagger API dokümantasyonu
- ✅ SOLID prensipleri
- ✅ Repository Pattern
- ✅ Unit of Work Pattern

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
- **Caching**: MemoryCacheService

### API Layer
- **Controllers**: AuthController, ProductsController
- **Middlewares**: ExceptionHandlingMiddleware
- **Extensions**: ServiceExtensions (JWT Configuration)

## Versiyon

- **v1.0.0** - İlk sürüm

## Lisans

MIT License
