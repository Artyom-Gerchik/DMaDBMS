using LAB6.Entities;
using LAB6.Models;
using LAB6.Models.Admin;
using LAB6.Models.Moderator;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace LAB6.Controllers;

public class ModeratorController : Controller
{
    private readonly NpgsqlCommand dbcommand;

    public ModeratorController()
    {
        dbcommand = DB_Manager.getCommand();
    }

    private ModeratorProfileModel GetModer()
    {
        var model = new ModeratorProfileModel();

        dbcommand.CommandText =
            @"SELECT users.id, users.login FROM users JOIN moderators ON users.login = (@p1) AND users.id = moderators.id;";

        var params1 = dbcommand.CreateParameter();

        params1.ParameterName = "p1";
        params1.Value = User.Identity.Name;

        dbcommand.Parameters.Add(params1);

        var dataReader = dbcommand.ExecuteReader();

        while (dataReader.Read())
        {
            model.id = (Guid?)dataReader.GetValue(dataReader.GetOrdinal("id"));
            model.Login = dataReader.GetValue(dataReader.GetOrdinal("login")).ToString();
        }

        return model;
    }

    [HttpGet]
    public IActionResult Profile()
    {
        return View(GetModer());
    }

    [HttpGet]
    public IActionResult Questions()
    {
        var model = new QuestionsListModel();
        model.QuestionModels = new List<QuestionModel>();

        dbcommand.CommandText =
            @"SELECT * FROM questions WHERE questions.status = false";

        var dataReader = dbcommand.ExecuteReader();
        while (dataReader.Read())
        {
            var tmp = new QuestionModel();
            tmp.Id = (Guid)dataReader.GetValue(0);
            tmp.ClientId = (Guid)dataReader.GetValue(1);
            tmp.Question = dataReader.GetValue(2) as string;
            tmp.Answer = dataReader.GetValue(3) as string;
            tmp.Status = (bool)dataReader.GetValue(4);

            model.QuestionModels.Add(tmp);
        }

        dataReader.Close();

        return View(model);
    }

    [HttpGet]
    public IActionResult Answer(Guid id)
    {
        var model = new QuestionModel();
        dbcommand.CommandText = @"SELECT * FROM questions WHERE id = (@p1)";

        var params1 = dbcommand.CreateParameter();

        params1.ParameterName = "p1";
        params1.Value = id;

        dbcommand.Parameters.Add(params1);

        var dataReader = dbcommand.ExecuteReader();

        while (dataReader.Read())
        {
            model.Id = (Guid)dataReader.GetValue(dataReader.GetOrdinal("id"));
            model.ClientId = (Guid)dataReader.GetValue(dataReader.GetOrdinal("client_id"));
            model.Question = dataReader.GetValue(dataReader.GetOrdinal("question")) as string;
            model.Answer = dataReader.GetName(dataReader.GetOrdinal("answer"));
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Answer(QuestionModel model, Guid id)
    {
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> Questions(Guid id)
    {
        return RedirectToAction("Answer", "Moderator");
    }

    [HttpGet]
    public IActionResult Clients()
    {
        var model = new ClientsModel();
        model.Clients = new List<Client>();

        dbcommand.CommandText =
            @"select users.id, users.first_name, users.last_name from users where fk_role_id = (select id from roles where role_name = 'client')";
        var dataReader = dbcommand.ExecuteReader();
        while (dataReader.Read())
        {
            var tmp = new Client();
            tmp.id = (Guid)dataReader.GetValue(0);
            tmp.FirstName = dataReader.GetValue(1) as string;
            tmp.LastName = dataReader.GetValue(2) as string;
            model.Clients.Add(tmp);
        }

        return View(model);
    }
}