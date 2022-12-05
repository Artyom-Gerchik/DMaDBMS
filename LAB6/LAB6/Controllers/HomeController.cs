using System.Data;
using System.Diagnostics;
using LAB6.Entities;
using Microsoft.AspNetCore.Mvc;
using LAB6.Models;
using Npgsql;

namespace LAB6.Controllers;

public class HomeController : Controller
{
    private readonly NpgsqlCommand dbcommand;

    public HomeController()
    {
        dbcommand = DB_Manager.getCommand();
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new AdminController().GetAllOffers());
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}