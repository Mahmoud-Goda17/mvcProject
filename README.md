# Company Management (ASP.NET Core MVC)

Three-tier company management web application: departments and employees, with account management and roles.

## Features

- Departments: full CRUD
- Employees: full CRUD with image upload, department assignment
- Accounts: sign up, sign in, forget/reset password by email (SMTP)
- Users and roles management with ASP.NET Core Identity (assign users to roles)
- AutoMapper profiles, generic repository + unit of work, EF Core migrations

## Structure

- `Company.G01.PL`: controllers, views (Razor), DTOs, mapping, email service
- `Company.G01.BLL`: repository interfaces and implementations, unit of work
- `Company.G01.DAL`: models, DbContext, configurations, migrations

## Tech

.NET 9, ASP.NET Core MVC, Entity Framework Core, SQL Server, ASP.NET Core Identity, AutoMapper

## Running locally

1. Prerequisites: .NET 9 SDK, SQL Server.
2. Update the connection string in `Company.G01.PL/appsettings.json`, and set SMTP credentials with `dotnet user-secrets` (never commit them).
3. `dotnet ef database update`, then run `Company.G01.PL`.
