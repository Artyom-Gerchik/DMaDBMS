using System.Diagnostics;
using System.Security.Claims;
using LAB6.Entities;
using Microsoft.AspNetCore.Mvc;
using LAB6.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Npgsql;

namespace LAB6.Controllers;

public class AccountController : Controller
{
    private readonly NpgsqlCommand dbcommand;

    public AccountController()
    {
        dbcommand = DB_Manager.getCommand();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            if (CheckForSignIn(model.Login, model.Password))
            {
                await SignInUser(model.Login);
                return RedirectToAction("Index", "Home");
            }
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync();
        HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
        return RedirectToAction("Index", "Home");
    }

    private bool CheckForSignIn(string login, string password)
    {
        dbcommand.CommandText = (@"SELECT * FROM users WHERE login = (@p1) AND password = (@p2)");

        var params1 = dbcommand.CreateParameter();
        var params2 = dbcommand.CreateParameter();

        params1.ParameterName = "p1";
        params1.Value = login;

        params2.ParameterName = "p2";
        params2.Value = password;

        dbcommand.Parameters.Add(params1);
        dbcommand.Parameters.Add(params2);

        NpgsqlDataReader dataReader = dbcommand.ExecuteReader();

        if (dataReader.Read() == false)
        {
            return false;
        }

        return true;
    }

    private async Task SignInUser(string login)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, login),
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
    }
}