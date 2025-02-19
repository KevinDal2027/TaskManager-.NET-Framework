using ASPWebApp.Dtos;
using ASPWebApp.Entities;

namespace ASPWebApp.Mapping;

public static class TaskMapping
{
    public static TaskItem ToEntity(this CreateTaskDto task) {
        return new TaskItem (){
            Name = task.Name,
            CategoryId = task.CategoryID,
            Priority = task.Priority,
            DueDate = task.DueDate
        };     
    }

    public static TaskSummaryDto ToTaskSummaryDto(this TaskItem task) {
        return new (
            task.Id,
            task.Name,
            task.Category!.Name, // Converts Category object to its name
            task.Priority,
            task.DueDate
        );
    }

    public static TaskDtoDetails ToTaskDetailsDto(this TaskItem task) {
        return new (
            task.Id,
            task.Name,
            task.CategoryId,
            task.Priority,
            task.DueDate
        );
    }

    public static TaskItem ToEntity(this UpdateTaskDto task, int id) {
        return new TaskItem (){
            Id = id,
            Name = task.Name,
            CategoryId = task.CategoryID,
            Priority = task.Priority,
            DueDate = task.DueDate
        };     
    }
}
