

namespace Infrastructure.Entities;

public class SubscriberEntity
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public bool IsSubscribed { get; set; }
    public bool Circle1 { get; set; }
    public bool Circle2 { get; set; }
    public bool Circle3 { get; set; }
    public bool Circle4 { get; set; }
    public bool Circle5 { get; set; }
    public bool Circle6 { get; set; }
}
