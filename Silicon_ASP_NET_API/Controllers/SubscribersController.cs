using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silicon_ASP_NET_API.Filters;

namespace Silicon_ASP_NET_API.Controllers;


[Route("api/[controller]")]
[ApiController]

public class SubscribersController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    #region CREATE
    [HttpPost]
    [UseApiKey]
    public async Task<IActionResult> Create(string email, bool isSubscribed, bool circle1, bool circle2, bool circle3, bool circle4, bool circle5, bool circle6)
    {
        if (!string.IsNullOrEmpty(email))
        {
            if (!await _context.Subscribers.AnyAsync(x => x.Email == email))
            {
                try
                {
                    var subscriberEntity = new SubscriberEntity
                    {
                        Email = email,
                        IsSubscribed = isSubscribed,
                        Circle1 = circle1,
                        Circle2 = circle2,
                        Circle3 = circle3,
                        Circle4 = circle4,
                        Circle5 = circle5,
                        Circle6 = circle6
                    };
                    _context.Subscribers.Add(subscriberEntity);
                    await _context.SaveChangesAsync();

                    return Created("", null);
                }
                catch
                {
                    return Problem("Unable to create subscription.");
                }
            }
            return Conflict("Email already exists.");
        }
        return BadRequest();
    }

    #endregion


    #region READ
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var subscribers = await _context.Subscribers.ToListAsync();

        if (subscribers.Count > 0)
        {
            return Ok(subscribers);
        }

        return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var subscriber = await _context.Subscribers.FindAsync(id); //move to repository

        if (subscriber != null)
        {
            return Ok(subscriber);
        }

        return NotFound();
    }

    #endregion

    #region UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOne(int id, string email)
    {
        var subscriber = await _context.Subscribers.FindAsync(id); //move to repository

        if (subscriber != null)
        {
            subscriber.Email = email;
            _context.Subscribers.Update(subscriber);
            await _context.SaveChangesAsync();

            return Ok(subscriber);
        }
        return NotFound();
    }

    #endregion

    #region DELETE

    [HttpDelete]
    [UseApiKey]
    public async Task<IActionResult> DeleteOne([FromQuery] string email)
    {
        var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Email == email);
        if (subscriber != null)
        {
            _context.Subscribers.Remove(subscriber);
            await _context.SaveChangesAsync();

            return Ok($"Subscriber with email '{email}' has been successfully deleted.");
        }

        return NotFound($"Subscriber with email '{email}' not found.");
    }

    #endregion
}
