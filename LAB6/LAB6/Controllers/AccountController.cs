using System.Data;
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

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (ModelState.IsValid)
            if (CheckForSignUp(model.Login))
            {
                await SignUpUser(model);
                return RedirectToAction("Profile", "Client");
            }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var role = "";
        if (ModelState.IsValid)
        {
            if (CheckForSignIn(model.Login, model.Password, out role))
            {
                await SignInUser(model.Login);
                if (role == "client")
                {
                    return RedirectToAction("Profile", "Client");
                }
                else if (role == "admin")
                {
                    return RedirectToAction("Profile", "Admin");
                }
                else if (role == "moderator")
                {
                    return RedirectToAction("Profile", "Moderator");
                }
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

    public string CheckRole(string login)
    {
        var role = "";

        dbcommand.CommandText = @"SELECT * FROM users
	JOIN roles ON users.fk_role_id = roles.id AND login = (@p1)";

        var params1 = dbcommand.CreateParameter();

        params1.ParameterName = "p1";
        params1.Value = login;

        dbcommand.Parameters.Add(params1);

        var dataReader = dbcommand.ExecuteReader();

        if (dataReader.Read() == false)
        {
            role = "";
            dbcommand.Parameters.Clear();
            return role;
        }

        role = dataReader.GetValue(dataReader.GetOrdinal("role_name")).ToString();
        dbcommand.Parameters.Clear();


        return role;
    }

    private bool CheckForSignIn(string login, string password, out string? role)
    {
        dbcommand.CommandText = @"SELECT * FROM users
	JOIN roles ON users.fk_role_id = roles.id AND login = (@p1) AND password = (@p2)";

        var params1 = dbcommand.CreateParameter();
        var params2 = dbcommand.CreateParameter();

        params1.ParameterName = "p1";
        params1.Value = login;

        params2.ParameterName = "p2";
        params2.Value = password;

        dbcommand.Parameters.Add(params1);
        dbcommand.Parameters.Add(params2);

        var dataReader = dbcommand.ExecuteReader();

        if (dataReader.Read() == false)
        {
            role = "";
            dbcommand.Parameters.Clear();
            return false;
        }

        role = dataReader.GetValue(dataReader.GetOrdinal("role_name")).ToString();
        dbcommand.Parameters.Clear();
        return true;
    }

    private bool CheckForSignUp(string login)
    {
        dbcommand.CommandText = @"SELECT * FROM users WHERE login = (@p1)";

        var params1 = dbcommand.CreateParameter();

        params1.ParameterName = "p1";
        params1.Value = login;

        dbcommand.Parameters.Add(params1);

        var dataReader = dbcommand.ExecuteReader();

        if (dataReader.Read() == false)
        {
            dataReader.Close();
            dbcommand.Parameters.Clear();
            return true;
        }

        return false;
    }

    private async Task SignUpUser(RegisterModel model)
    {
        //dbcommand.CommandType = CommandType.StoredProcedure;
        dbcommand.CommandText = @"CALL registerClient((@p1), (@p2), (@p3), (@p4), (@p5), cast((@p6) as date))";
        var params1 = dbcommand.CreateParameter();
        var params2 = dbcommand.CreateParameter();
        var params3 = dbcommand.CreateParameter();
        var params4 = dbcommand.CreateParameter();
        var params5 = dbcommand.CreateParameter();
        var params6 = dbcommand.CreateParameter();

        params1.ParameterName = "p1";
        params1.Value = model.Login;

        params2.ParameterName = "p2";
        params2.Value = model.Password;

        params3.ParameterName = "p3";
        params3.Value = model.FirstName;

        params4.ParameterName = "p4";
        params4.Value = model.LastName;

        params5.ParameterName = "p5";
        params5.Value = model.Patronymic;

        params6.ParameterName = "p6";
        params6.Value = model.DateOfBirth;

        dbcommand.Parameters.Add(params1);
        dbcommand.Parameters.Add(params2);
        dbcommand.Parameters.Add(params3);
        dbcommand.Parameters.Add(params4);
        dbcommand.Parameters.Add(params5);
        dbcommand.Parameters.Add(params6);

        dbcommand.ExecuteReader();
        dbcommand.Parameters.Clear();

        await SignInUser(model.Login);
    }

    private async Task SignInUser(string login)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, login)
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
    }
}