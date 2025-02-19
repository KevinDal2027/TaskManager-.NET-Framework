using System.ComponentModel.DataAnnotations;

namespace ASPWebApp.Dtos;

public record class UpdateTaskDto(
    [Required][StringLength(100)] string Name, 
    int CategoryID,      // Replaced GenreID with CategoryID for Academics (1) or Others (2)
    [Range(1, 100)] int Priority,  // Changed Price to Priority (1-100)
    DateOnly DueDate     // Changed ReleaseDate to DueDate for task deadline
);
