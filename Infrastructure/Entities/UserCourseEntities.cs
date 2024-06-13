

using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class UserCourseEntities
{
    [Key]
    public int UserCourseId { get; set; }

    public string UserId { get; set; } = null!;

    public int CourseId { get; set; }

    public CourseEntity? Course { get; set; }
}
