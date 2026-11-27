# Architecture
```mermaid
flowchart TD
Browser --> MVC[Controllers / Razor Views]
Client[API client] --> API[API Controllers]
MVC --> Services[Scope / business validation / audit]
API --> Services
Services --> EF[EF Core DbContext]
EF --> SQL[(SQL Server)]
Identity[ASP.NET Core Identity] --> EF
```
Identity validates credentials and generates session cookies. Controllers use explicit input models and scoped EF queries to prevent unauthorized records being returned. Audit changes are recorded alongside CRM writes.
