using LAB6.Entities;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using LAB6.Models;

namespace LAB6.Controllers;

public class ClientController : Controller
{
    private readonly NpgsqlCommand dbcommand;

    public ClientController()
    {
        dbcommand = DB_Manager.getCommand();
    }

    public ClientProfileModel GetClient()
    {
        var model = new ClientProfileModel();

        dbcommand.CommandText =
            @"SELECT users.id, users.login, users.password, users.first_name, users.last_name, clients.patronymic, clients.date_of_birth FROM users
	JOIN clients ON users.login = (@p1) AND users.id = clients.id;";

        var params1 = dbcommand.CreateParameter();

        params1.ParameterName = "p1";
        params1.Value = User.Identity.Name;

        dbcommand.Parameters.Add(params1);

        var dataReader = dbcommand.ExecuteReader();

        while (dataReader.Read())
        {
            model.id = (Guid?)dataReader.GetValue(dataReader.GetOrdinal("id"));
            model.Login = dataReader.GetValue(dataReader.GetOrdinal("login")).ToString();
            model.Password = dataReader.GetValue(dataReader.GetOrdinal("password")).ToString();
            model.FirstName = dataReader.GetValue(dataReader.GetOrdinal("first_name")).ToString();
            model.LastName = dataReader.GetValue(dataReader.GetOrdinal("last_name")).ToString();
            model.Patronymic = dataReader.GetValue(dataReader.GetOrdinal("patronymic")).ToString();
            model.DateOfBirth =
                DateOnly.FromDateTime((DateTime)dataReader.GetValue(dataReader.GetOrdinal("date_of_birth")));
        }

        dataReader.Close();
        dbcommand.Parameters.Clear();

        return model;
    }

    [HttpGet]
    public IActionResult UpdateClient()
    {
        var model = GetClient();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateClient(ClientProfileModel model)
    {
        var client = GetClient();

        dbcommand.CommandText =
            @"UPDATE users SET first_name = (@p1), last_name = (@p2) WHERE login = (@p3) AND id = (@p4);
               UPDATE clients SET patronymic = (@p5) WHERE id = (@p4)";

        var params1 = dbcommand.CreateParameter();
        var params2 = dbcommand.CreateParameter();
        var params3 = dbcommand.CreateParameter();
        var params4 = dbcommand.CreateParameter();
        var params5 = dbcommand.CreateParameter();

        params1.ParameterName = "p1";
        params1.Value = model.FirstName;

        params2.ParameterName = "p2";
        params2.Value = model.LastName;

        params3.ParameterName = "p3";
        params3.Value = client.Login;

        params4.ParameterName = "p4";
        params4.Value = client.id;

        params5.ParameterName = "p5";
        params5.Value = model.Patronymic;

        dbcommand.Parameters.Add(params1);
        dbcommand.Parameters.Add(params2);
        dbcommand.Parameters.Add(params3);
        dbcommand.Parameters.Add(params4);
        dbcommand.Parameters.Add(params5);

        dbcommand.ExecuteReader();
        dbcommand.Parameters.Clear();
        return RedirectToAction("Profile", "Client");
    }

    [HttpGet]
    public IActionResult Profile()
    {
        return View(GetClient());
    }
}