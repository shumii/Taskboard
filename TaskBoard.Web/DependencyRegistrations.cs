using TaskBoard.Application.Services;
using TaskBoard.Data.Repository;
using TaskBoard.Domain.Repository;

namespace TaskBoard.Web
{
    public static class DependencyRegistrations
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITaskBoardRepository, TaskboardRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITaskBoardService, TaskBoardService>();
        }
    }
}
