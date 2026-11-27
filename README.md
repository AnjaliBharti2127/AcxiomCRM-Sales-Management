# AcxiomCRM
ASP.NET Core MVC / .NET 10 / SQL Server / EF Core / Identity / Bootstrap / Chart.js.

**Status:** generated source implementation, not compiled or runtime-tested. Do not treat this as submission-ready until local verification. The build environment had no .NET SDK or SQL Server. See docs/REQUIREMENTS_CHECKLIST.md for gaps.

## Windows quick start
Install .NET 10 SDK and SQL Server LocalDB (Visual Studio Installer: ASP.NET and web development plus SQL Server Express LocalDB). Extract ZIP, open PowerShell in the outer AcxiomCRM folder, and run:

```powershell
powershell -ExecutionPolicy Bypass -File .\setup.ps1
```

Choose your own strong demo password when prompted. The script restores packages, builds, trusts the development HTTPS certificate, creates the initial migration, updates LocalDB, saves the demo password via user-secrets, and starts the application. If using SQL Server Express instead, first run `dotnet user-secrets set "ConnectionStrings:Default" "Server=.\SQLEXPRESS;Database=AcxiomCRM;Trusted_Connection=True;TrustServerCertificate=True" --project AcxiomCRM`.

Expected launch profile: https://localhost:7240 (not verified). Open the URL printed by dotnet run. Accounts: admin@example.test, manager1@example.test, manager2@example.test, sales1@example.test, sales2@example.test. All demo accounts use the password you chose. Seed only runs in Development with Demo:Enabled=true. Disable it after setup. Roles and initial admin for non-development environments require a separate controlled provisioning process.

## Run again
`dotnet run --project AcxiomCRM`

## Test
`dotnet test AcxiomCRM.Tests` (unit tests included, not executed here). Manual scenarios: docs/TESTING.md.

## Structure
Models hold database entities, ViewModels contain explicit input DTOs, Data defines EF/Identity and seed, Services provides scope and validation/audit, Controllers handles routes, Views renders HTML. API controllers share the same scoped services. UI assets load from CDNs (internet required for full styling, validation and charts).

## API
Development Swagger: /swagger. Cookie authentication requires CSRF tokens for writes. See docs/API.md.

## Production
Use current patched compatible packages, a secure SQL connection, proper secrets, HTTPS, private audit retention and a full security review. Demo setup is not a production deployment. Do not publish the assignment PDF or real customer information without authorization.
