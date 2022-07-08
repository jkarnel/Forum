# Forum

Simple Forum application. User can create posts and reply on existing ones.

Solution contains next projects:
1. Forum.Core - core objects + potential business logic
2. Forum.Data - db mappings, configuration
3. Forum.Services.Abstract - interfaces and DTOs for backend services
4. Forum.Services - backend services
5. Forum.Utilities - common utility classes
6. Forum.Web - MVC web project

### Build and run
1. Asp .net core 3.1 should be installed
2. Visual studio (2017+)
3. Build main Forum.Web project
4. Change db connection string in appsettings.json (DefaultConnection).
5. Run
