using ASPWebApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPWebApp.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app) {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TaskStoreContext>();
        await dbContext.Database.MigrateAsync();
    }
}
