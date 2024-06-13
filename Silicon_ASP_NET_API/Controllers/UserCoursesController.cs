using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silicon_ASP_NET_API.Filters;

namespace Silicon_ASP_NET_API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserCoursesController : ControllerBase
{
    private readonly DataContext _context;

    public UserCoursesController(DataContext context)
    {
        _context = context;
    }

    [UseApiKey]
    [HttpPost]
    public async Task<ActionResult<UserCourseModel>> PostUserCourse(UserCourseModel userCourse)
    {
        try
        {
            var existingUserCourse = await _context.UserCourses
                .FirstOrDefaultAsync(uc => uc.UserId == userCourse.UserId && uc.CourseId == userCourse.CourseId);

            if (existingUserCourse != null)
            {
                return Conflict("Course already exists for this user.");
            }

            var userCourseEntity = new UserCourseEntities
            {
                UserId = userCourse.UserId!,
                CourseId = userCourse.CourseId
            };

            _context.UserCourses.Add(userCourseEntity);
            await _context.SaveChangesAsync();

            return CreatedAtRoute(new { }, new { message = "Course added successfully" });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Failed to add course. Please try again later.");
        }
    }

    [UseApiKey]
    [HttpGet("{userId}")]
    public async Task<ActionResult<List<UserSavedCourseModel>>> GetSavedCourses(string userId)
    {
        try
        {

            var savedCourses = await _context.UserCourses
                .Where(x => x.UserId == userId)
                .Select(x => new UserSavedCourseModel
                {
                    CourseId = x.CourseId,
                    Title = x.Course!.Title,
                    Price = x.Course.Price,
                    DiscountPrice = x.Course.DiscountPrice,
                    Hours = x.Course.Hours,
                    IsBestseller = x.Course.IsBestseller,
                    LikesInNumbers = x.Course.LikesInNumbers,
                    LikesInProcent = x.Course.LikesInProcent,
                    Author = x.Course.Author,
                    Img = x.Course.Img

                })
                .ToListAsync();

            return Ok(savedCourses);
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Failed to retrieve saved courses. Please try again later.");
        }
    }

    [UseApiKey]
    [HttpDelete("{userId}/{courseId}")]
    public async Task<IActionResult> DeleteUserCourse(string userId, int courseId)
    {
        try
        {
            var userCourse = await _context.UserCourses.FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);

            if (userCourse == null)
            {
                return NotFound("User course not found.");
            }

            _context.UserCourses.Remove(userCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Failed to delete user course. Please try again later.");
        }
    }

    [UseApiKey]
    [HttpDelete("all/{userId}")]
    public async Task<IActionResult> DeleteAllUserCourses(string userId)
    {
        try
        {
            var userCourses = await _context.UserCourses.Where(uc => uc.UserId == userId).ToListAsync();

            if (!userCourses.Any())
            {
                return NotFound("No courses found for the user.");
            }

            _context.UserCourses.RemoveRange(userCourses);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Failed to delete user courses. Please try again later.");
        }
    }
}
