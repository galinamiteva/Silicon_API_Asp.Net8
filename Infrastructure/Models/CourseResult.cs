

namespace Infrastructure.Models;

public class CourseResult
{
    public bool Succeeded { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<Course>? Courses { get; set; }
}
