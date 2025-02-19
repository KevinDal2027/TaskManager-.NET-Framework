namespace ASPWebApp.Dtos;

public record class TaskSummaryDto(
    int Id,
    string Name, 
    string Category, // Replacing Genre with Category (Academics / Others)
    int Priority,    // Replacing Price with Priority (1-100)
    DateOnly DueDate // Replacing ReleaseDate with DueDate
);
