using ASPWebApp.Data;
using ASPWebApp.Mapping;
using Microsoft.EntityFrameworkCore;

namespace ASPWebApp.Endpoints;

public static class CategoryEndpoints
{
    public static RouteGroupBuilder MapCategoryEndpoint(this WebApplication app) {
        var group = app.MapGroup("categories");
        
        group.MapGet("/", async (TaskStoreContext dbContext) =>
            await dbContext.Categories
                .Select(category => category.ToDto())
                .AsNoTracking()
                .ToListAsync()
        );
        
        return group;
    }
}
