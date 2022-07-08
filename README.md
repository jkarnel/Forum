# Forum

Simple Forum application. User can create posts and reply on existing ones.

Solution contains next projects.
Forum.Core - core objects + potential business logic
Forum.Data - db mappings, configuration
Forum.Services.Abstract - interfaces and DTOs for backend services
Forum.Services - backend services
Forum.Utilities - common utility classes
Forum.Web - MVC web project

##Build and run
1. Asp .net core 3.1 should be installed
2. Visual studio (2017+)
3. Build main Forum.Web project
4. Change db connection string in appsettings.json (DefaultConnection).
5. Run
