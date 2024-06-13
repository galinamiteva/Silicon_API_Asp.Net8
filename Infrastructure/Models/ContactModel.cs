
namespace Infrastructure.Models;

public class ContactModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? SelectedService { get; set; }
    public string Message { get; set; } = null!;
}
