using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ASPWebApp.Data;
using ASPWebApp.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy before building the app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add database context and other services
var connString = builder.Configuration.GetConnectionString("TaskStore");
builder.Services.AddSqlite<TaskStoreContext>(connString);

var app = builder.Build();

// Apply the CORS policy to the app
app.UseCors("AllowAll");

app.MapTasksEndpoints();
app.MapCategoryEndpoint();

await app.MigrateDbAsync();

app.Run();
