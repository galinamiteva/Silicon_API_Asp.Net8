

namespace Infrastructure.Models;

public class UserSavedCourseModel
{
    public int CourseId { get; set; }
    public string? Title { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountPrice { get; set; }
    public int Hours { get; set; }
    public bool IsBestseller { get; set; }
    public decimal LikesInNumbers { get; set; }
    public decimal LikesInProcent { get; set; }
    public string? Author { get; set; }
    public string? Img { get; set; }
}
