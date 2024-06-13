


using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factory;

public class CategoryFactory
{
    public static Category Create(CategoryEntity entity)
    {
        try
        {
            return new Category
            {
                Id = entity.Id,
                CategoryName = entity.CategoryName,
            };
        }
        catch { }
        return null!;
    }

    public static IEnumerable<Category> Create(List<CategoryEntity> entities)
    {
        List<Category> categories = [];
        try
        {
            foreach (var entity in entities)
            {
                categories.Add(Create(entity));
            }


        }
        catch { }
        return categories;
    }

}