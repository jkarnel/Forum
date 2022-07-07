using Forum.Data;
using Forum.Services;
using Forum.Services.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Web.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            using var dbContext = serviceScope.ServiceProvider.GetRequiredService<ForumDbContext>();

            dbContext.Database.Migrate();

            return app;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IReplyService, ReplyService>();

            return services;
        }
    }
}
