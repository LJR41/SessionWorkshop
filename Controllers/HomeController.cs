using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("create/user")]
    public IActionResult CreateUser(string name)
    {
        HttpContext.Session.SetString("name", name);
        HttpContext.Session.SetInt32("Sum", 22);
        return RedirectToAction("dashboard");

    }

    [HttpGet("dashboard")]
    public ViewResult Dashboard()
    {
        string? Name = HttpContext.Session.GetString("Name");
        int? MySum = HttpContext.Session.GetInt32("Sum");
        Console.WriteLine(MySum);
        return View("Dashboard");
    }

    [HttpGet("logout")]
    public ViewResult Logout()
    {
        HttpContext.Session.Clear();
        return View("Index");
    }

    [HttpGet("oneforall")]
    public IActionResult OneForAll(string name)
    {
        if (name == "PlusOne")
        {
            int? OldSum = HttpContext.Session.GetInt32("Sum");
            int? NewSum = OldSum + 1;
            HttpContext.Session.SetInt32("Sum", (int)NewSum);
            return View("Dashboard");
        }
        if (name == "MinusOne")
        {
            int? OldSum = HttpContext.Session.GetInt32("Sum");
            int? NewSum = OldSum - 1;
            HttpContext.Session.SetInt32("Sum", (int)NewSum);
            return View("Dashboard");
        }
        if (name == "TimesTwo")
        {
            int? OldSum = HttpContext.Session.GetInt32("Sum");
            int? NewSum = OldSum * 2;
            HttpContext.Session.SetInt32("Sum", (int)NewSum);
            return View("Dashboard");
        }
        if (name == "Random")
        {
            Random rand = new();
            int? OldSum = HttpContext.Session.GetInt32("Sum");
            int? NewSum = OldSum + rand.Next(1, 11);
            HttpContext.Session.SetInt32("Sum", (int)NewSum);
            return View("Dashboard");
        }
        return View("Dashboard");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
