# Copilot Coding Agent Instructions

## Project Overview

This is a Clean Architecture ASP.NET Core 10 application with the following structure:

- `src/Domain/` — Domain entities and business logic (TodoList, TodoItem)
- `src/Application/` — Application layer with MediatR commands/queries, validators, DTOs
- `src/Infrastructure/` — EF Core data access, Identity, external services
- `src/Web/` — ASP.NET Core Web API endpoints, middleware, configuration

## Architecture Rules

- Follow Clean Architecture — dependencies flow inward (Web → Application → Domain)
- Use MediatR for commands and queries (CQRS pattern)
- Use FluentValidation for input validation in the Application layer
- Entity Framework Core with PostgreSQL is the database provider
- All API endpoints are defined in `src/Web/Endpoints/` as minimal API endpoint classes

## Testing

- Tests are in `tests/` using NUnit, Moq, and Shouldly
- Run tests with: `dotnet test` (exclude AcceptanceTests)
- Always ensure existing tests pass after making changes
- Add tests for new functionality when appropriate

## Code Style

- Use C# 12 features (file-scoped namespaces, primary constructors where appropriate)
- Use `var` for local variables when the type is obvious
- Follow existing naming conventions in the codebase
- Keep methods small and focused

## Common Patterns

- New API endpoints: Create an endpoint class in `src/Web/Endpoints/`
- New features: Add Command/Query in `src/Application/`, handler, and validator
- Bug fixes: Fix in the appropriate layer, ensure tests pass
- Database changes: Modify entities in Domain, update DbContext in Infrastructure

## Important Notes

- The database uses PostgreSQL (Npgsql provider) — not SQLite or SQL Server
- The `EnsureCreatedAsync()` call in `ApplicationDbContextInitialiser` handles schema creation
- Do NOT use EF Core migrations — the project uses `EnsureCreatedAsync()` instead
- The CI pipeline runs on every PR — make sure `dotnet build` and `dotnet test` pass
