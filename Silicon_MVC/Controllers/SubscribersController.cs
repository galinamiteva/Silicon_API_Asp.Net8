using Microsoft.AspNetCore.Mvc;
using Silicon_MVC.Models;

namespace Silicon_MVC.Controllers;

public class SubscribersController : Controller
{
    public IActionResult Index()
    {
        return View(new SubscribeViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(SubscribeViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            using var http = new HttpClient();

            var url = $"https://localhost:7029/api/subscribers?email={viewModel.Email}";
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var response = await http.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                viewModel.IsSubscribed = true;
            }
        }

        return View(viewModel);
    }
}
