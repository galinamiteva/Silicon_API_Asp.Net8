using Infrastructure.Entities;

namespace Silicon_ASP_NET_API.Models;

public class CourseCreate
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal DiscountPrice { get; set; }
    public int Hours { get; set; }
    public bool IsBestseller { get; set; }
    public decimal LikesInNumbers { get; set; }
    public decimal LikesInProcent { get; set; }
    public string? Author { get; set; }
    public string? Img { get; set; } = null!;
    public int? CategoryId { get; set; }

    public static implicit operator CourseCreate(CourseEntity courseEntity)
    {
        return new CourseCreate
        {
            Id = courseEntity.Id,
            Title = courseEntity.Title,
            Price = courseEntity.Price,
            DiscountPrice = courseEntity.DiscountPrice,
            Hours = courseEntity.Hours,
            IsBestseller = courseEntity.IsBestseller,
            LikesInNumbers = courseEntity.LikesInNumbers,
            LikesInProcent = courseEntity.LikesInProcent,
            Author = courseEntity.Author,
            Img = courseEntity.Img,
            CategoryId = courseEntity.Category?.Id
        };
    }
}
