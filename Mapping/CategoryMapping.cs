using ASPWebApp.Dtos;
using ASPWebApp.Entities;

namespace ASPWebApp.Mapping;

public static class CategoryMapping
{
    public static CategoryDto ToDto(this Category category) {
        return new CategoryDto(category.Id, category.Name);
    }
}
