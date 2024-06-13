using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon_MVC.Models;
using System.Text;

namespace Silicon_MVC.Controllers;

public class CoursesController : Controller
{
    public async Task<IActionResult> Index()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://localhost:7029/api/courses");
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<IEnumerable<CourseEntity>>(json);

        return View(data);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CourseRegistrationFormViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            using var http = new HttpClient();

            var json = JsonConvert.SerializeObject(viewModel);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await http.PostAsync($"https://localhost:7029/api/courses", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Courses");
            }

        }

        return View();
    }
}
