using System.ComponentModel.DataAnnotations;

namespace Silicon_ASP_NET_API.Dtos;

public class CourseRegistrationForm
{
    [Required]
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal DiscountPrice { get; set; }
    public int Hours { get; set; }
    public bool IsBestseller = false;
    public decimal LikesInNumbers { get; set; }
    public decimal LikesInProcent { get; set; }
    public string? Author { get; set; }
    public string? Img { get; set; }


   
}
