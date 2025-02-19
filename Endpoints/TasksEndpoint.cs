using ASPWebApp.Data;
using ASPWebApp.Dtos;
using ASPWebApp.Entities;
using ASPWebApp.Mapping;
using Microsoft.EntityFrameworkCore;

namespace ASPWebApp.Endpoints;

public static class TasksEndpoints {

    const string GetTaskEndpointName = "GetTask";

    public static RouteGroupBuilder MapTasksEndpoints(this WebApplication app) {
        var group = app.MapGroup("tasks").WithParameterValidation();
        
        // Get all tasks
        group.MapGet("/", async (TaskStoreContext dbContext) => 
            await dbContext.Tasks
                .Select(task => task.ToTaskDetailsDto())
                .AsNoTracking()
                .ToListAsync());

        // Get task by ID
        group.MapGet("/{id}", async (int id, TaskStoreContext dbContext) => {
            TaskItem? task = await dbContext.Tasks.FindAsync(id);

            return task is null ? Results.NotFound() : Results.Ok(task.ToTaskDetailsDto());
        }).WithName(GetTaskEndpointName);

        // Add a new task
        group.MapPost("/", async (CreateTaskDto newTask, TaskStoreContext dbContext) =>
        {
            TaskItem task = newTask.ToEntity();
            dbContext.Tasks.Add(task);
            await dbContext.SaveChangesAsync();
            TaskDtoDetails taskDto = task.ToTaskDetailsDto();

            return Results.CreatedAtRoute(GetTaskEndpointName, new { id = task.Id }, taskDto);
        });

        // Update an existing task
        group.MapPut("/{id}", async (int id, UpdateTaskDto updatedTask, TaskStoreContext dbContext) =>
        {
            var existingTask = await dbContext.Tasks.FindAsync(id);

            if (existingTask is null) return Results.NotFound();
            
            dbContext.Entry(existingTask).CurrentValues.SetValues(updatedTask.ToEntity(id));

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        // Delete a task
        group.MapDelete("/{id}", async (int id, TaskStoreContext dbContext) => {
            await dbContext.Tasks.Where(task => task.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return group;
    }
}
