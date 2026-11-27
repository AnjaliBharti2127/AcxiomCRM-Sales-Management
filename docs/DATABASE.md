# Database
```mermaid
erDiagram
APPLICATION_USER ||--o{ CUSTOMER : owns
APPLICATION_USER ||--o{ LEAD : owns
APPLICATION_USER ||--o{ OPPORTUNITY : owns
APPLICATION_USER ||--o{ FOLLOWUP : owns
APPLICATION_USER ||--o{ ACTIVITY : owns
CUSTOMER ||--o{ OPPORTUNITY : relates
LEAD o|--o{ OPPORTUNITY : originates
CUSTOMER o|--o{ FOLLOWUP : relates
LEAD o|--o{ FOLLOWUP : relates
OPPORTUNITY o|--o{ FOLLOWUP : relates
CUSTOMER o|--o{ ACTIVITY : relates
LEAD o|--o{ ACTIVITY : relates
```
EF Core creates SQL Server tables, foreign keys and indexes via local migrations. Identity manages password hashes and role tables. Soft deletion preserves referenced records. Customer normalized email and phone have unique indexes.
