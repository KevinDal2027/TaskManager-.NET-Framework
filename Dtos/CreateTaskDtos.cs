using System.ComponentModel.DataAnnotations;

namespace ASPWebApp.Dtos;

public record class CreateTaskDto (
    [Required][StringLength(50)] string Name, 
    [Required] int CategoryID, // 1 = Academics, 2 = Others
    [Range(1, 100)] int Priority, // Instead of Price, now Priority from 1-100
    [Required] DateOnly DueDate // Instead of ReleaseDate, now DueDate
);
