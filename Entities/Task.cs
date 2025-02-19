namespace ASPWebApp.Entities;

public class TaskItem {
    public int Id { get; set; } // Primary Key
    public required string Name { get; set; } // Task Name

    public int CategoryId { get; set; } // 1 = Academics, 2 = Others
    public Category? Category { get; set; } // Navigation Property

    public int Priority { get; set; } // 1-100 Priority Scale

    public DateOnly DueDate { get; set; } // Task Deadline
}
