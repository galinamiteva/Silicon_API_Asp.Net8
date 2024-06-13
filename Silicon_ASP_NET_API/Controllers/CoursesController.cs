using Infrastructure.Contexts;

using Infrastructure.Entities;
using Infrastructure.Factory;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silicon_ASP_NET_API.Dtos;
using Silicon_ASP_NET_API.Filters;
using Silicon_ASP_NET_API.Models;

namespace Silicon_ASP_NET_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [UseApiKey]
    [HttpGet]
    public async Task<IActionResult> GetAll(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 9)
    {
        if (ModelState.IsValid)
        {
            var query = _context.Courses.Include(i => i.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(category) && category != "all")
                query = query.Where(x => x.Category!.CategoryName == category);

            if (!string.IsNullOrEmpty(searchQuery))
                query = query.Where(x => x.Title.Contains(searchQuery) || x.Author!.Contains(searchQuery));

            query = query.OrderByDescending(o => o.Title);
            var courses = await query.ToListAsync();

            var response = new CourseResult
            {
                Succeeded = true,
                TotalItems = await query.CountAsync(),
            };

            response.TotalPages = (int)Math.Ceiling(response.TotalItems / (double)pageSize);
            response.Courses = CourseFactory.Create(await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync());

            return Ok(response);
        }
        else
        {
            return BadRequest(ModelState);
        }
    }


    [UseApiKey]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);

        if (course != null)
        {
            return Ok(course);
        }

        return NotFound();
    }

    //[UseApiKey]
    //[Authorize]
    [HttpPost]



   

    public async Task<IActionResult> Create(Course form)
    {
        if (ModelState.IsValid)
        {
            var courseEntity = new CourseEntity
            {
                Title = form.Title,
                Price = form.Price,
                DiscountPrice = form.DiscountPrice,
                Hours = form.Hours,
                IsBestseller = form.IsBestseller,
                LikesInNumbers = form.LikesInNumbers,
                LikesInProcent = form.LikesInProcent,
                Author = form.Author,
                Img = form.Img,
                CategoryId = form.CategoryId

            };
            _context.Courses.Add(courseEntity);
            await _context.SaveChangesAsync();

            return Created("", (CourseCreate)courseEntity);
        }
        return BadRequest();
    }

    //[UseApiKey]
    //[Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Course model)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course == null)
        {
            return NotFound();
        }

        course.Title = model.Title;
        course.Author = model.Author;

        _context.Courses.Update(course);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    //[UseApiKey]
    //[Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

        var course = await _context.Courses.FindAsync(id);

        if (course == null)
        {
            return NotFound();
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
