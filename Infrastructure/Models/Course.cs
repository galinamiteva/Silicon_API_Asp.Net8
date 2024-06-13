
using Infrastructure.Entities;

namespace Infrastructure.Models;

public class Course
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
    public string? Img { get; set; }
    public string? Category { get; set; } = null!;
    public int? CategoryId { get; set; }


    //public static implicit operator CourseEntity(Course model)
    //{
    //    return new CourseEntity
    //    {
    //        Title = model.Title,
    //        Author = model.Author,
    //        Price = model.Price,
    //        DiscountPrice = model.DiscountPrice,
    //        Hours = model.Hours,
    //        LikesInNumbers = model.LikesInNumbers,
    //        LikesInProcent = model.LikesInProcent,
    //        IsBestseller = model.IsBestseller,
    //        Img = model.Img,

    //        Category = new CategoryEntity
    //        {
    //            CategoryName = model.Category!
    //        }
    //    };
    //}
}
