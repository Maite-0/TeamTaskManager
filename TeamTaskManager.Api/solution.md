
# TeamTaskManager  – Design Overview

## Architecture

- **Layers:** Domain, Infrastructure (EF Core InMemory + repositories), API (Controllers, DTOs, AutoMapper)  
- **Repository Pattern:** Controllers do not access EF Core directly  
- **DTOs + AutoMapper:** Separates API responses from domain models
- **Test Layer:** xUnit test project with mocked dependencies

## Domain Modeling

- **TaskItem:** Id, Title, Description, Status, Priority, CreatedAt, `List<TeamMember> Assignees`  
- **TeamMember:** Id, Name, Email, `List<TaskItem> Tasks`  
- **Enums:** TaskStatus (Todo, InProgress, Done), TaskPriority (Low, Medium, High)  
- **Reasoning:** Many-to-many task assignment models real-world collaboration


## Validation & Error Handling

- **Data Annotations** ensure valid input on DTOs  
- **Global Exception Middleware** returns JSON with status & message  
- Controllers return 404 for not found entities


## Filtering, Searching, Sorting, Pagination

- **Filtering:** Status, Priority, AssigneeId  
- **Searching:** Title keyword  
- **Sorting:** Title, Priority, CreatedAt  
- **Pagination:** `Skip((PageNumber-1)*PageSize).Take(PageSize)`  
- **Design:** Efficient queries in repository; can combine multiple operations


## Seeded Data

Seeded data helps demonstrate:
- Many-to-many task assignment
- Pagination and filtering in realistic scenarios


## Trade-offs & Notes

- InMemory DB resets on restart; suitable for demo  
- Repository + AutoMapper adds abstraction and testability  
- AutoMapper added for DTO mapping
- Future enhancements: real DB persistence, auth, task history, unit tests

