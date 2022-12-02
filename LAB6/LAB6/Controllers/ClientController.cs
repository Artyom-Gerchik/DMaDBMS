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
        object id;

        dbcommand.CommandText =
            (@"SELECT users.id, users.login, users.password, users.first_name, users.last_name, clients.patronymic, clients.date_of_birth FROM users
	JOIN clients ON users.first_name = (@p1) AND users.id = clients.id;");

        var params1 = dbcommand.CreateParameter();

        params1.ParameterName = "p1";
        params1.Value = User.Identity.Name;

        dbcommand.Parameters.Add(params1);

        NpgsqlDataReader dataReader = dbcommand.ExecuteReader();


        while (dataReader.Read())
        {
            id = dataReader.GetValue(dataReader.GetOrdinal("id"));
            model.Login = dataReader.GetValue(dataReader.GetOrdinal("login")).ToString();
            model.Password = dataReader.GetValue(dataReader.GetOrdinal("password")).ToString();
            model.FirstName = dataReader.GetValue(dataReader.GetOrdinal("first_name")).ToString();
            model.LastName = dataReader.GetValue(dataReader.GetOrdinal("last_name")).ToString();
            model.Patronymic = dataReader.GetValue(dataReader.GetOrdinal("patronymic")).ToString();
            model.DateOfBirth =
                DateOnly.FromDateTime((DateTime)dataReader.GetValue(dataReader.GetOrdinal("date_of_birth")));
        }

        return model;
    }

    [HttpGet]
    public IActionResult Profile()
    {
        return View(GetClient());
    }
}