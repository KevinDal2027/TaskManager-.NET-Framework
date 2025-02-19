namespace ASPWebApp.Dtos;

public record class TaskDtoDetails(
    int Id,
    string Name, 
    int CategoryId,  // 1 = Academics, 2 = Others
    int Priority,    // Replacing Price with Priority (1-100)
    DateOnly DueDate // Replacing ReleaseDate with DueDate
);
