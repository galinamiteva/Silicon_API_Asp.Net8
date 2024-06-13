using Infrastructure.Contexts;

using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Silicon_ASP_NET_API.Filters;

namespace Silicon_ASP_NET_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpPost]
    [UseApiKey]

    public async Task<IActionResult> Create(ContactModel contactModel)
    {
        if (ModelState.IsValid)
        {
            var contactEntity = new ContactEntity
            {
                FullName = contactModel.FullName,
                Email = contactModel.Email,
                SelectedService = contactModel.SelectedService,
                Message = contactModel.Message
            };

            _context.Contacts.Add(contactEntity);
            await _context.SaveChangesAsync();

            return CreatedAtRoute(new { }, new { message = "Contact added successfully" });
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

}
