using System.ComponentModel.DataAnnotations;

namespace Silicon_MVC.Models;

public class SubscribeViewModel
{
    [Required]
    [Display(Name = "Subscribe", Prompt = "Enter your email address")]
    public string Email { get; set; } = null!;
    public bool IsSubscribed { get; set; } = false;
}
