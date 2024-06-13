
using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factory;

public class CourseFactory
{
    public static Course Create(CourseEntity entity)
    {
        try
        {
            return new Course
            {
                Id = entity.Id,
                Title = entity.Title,
                Price = entity.Price,
                DiscountPrice = entity.DiscountPrice,
                Hours = entity.Hours,
                IsBestseller = entity.IsBestseller,
                LikesInNumbers = entity.LikesInNumbers,
                LikesInProcent = entity.LikesInProcent,
                Author = entity.Author,
                Img = entity.Img,
                CategoryId = entity.CategoryId


            };
        }
        catch { }
        return null!;
    }

    public static IEnumerable<Course> Create(List<CourseEntity> entities)
    {
        List<Course> courses = [];
        try
        {
            foreach (var entity in entities)
            {
                courses.Add(Create(entity));
            }
        }
        catch { }
        return courses;
    }
}
