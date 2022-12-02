﻿using System.Data;
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
        dbcommand.CommandText = "SELECT id FROM users";
        // command.CommandText = "CALL registerClient('ddddddd', 'ddddddd', 'ddddddd', 'ddddddd', 'ddddddd', '1945-01-01');";

        NpgsqlDataReader dataReader = dbcommand.ExecuteReader();
        
        while (dataReader.Read())
        {
            Console.WriteLine(dataReader.GetGuid(0).ToString());
        }
        
        
        return View();
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