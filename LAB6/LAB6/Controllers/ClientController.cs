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

    [HttpGet]
    public IActionResult Order(Guid id)
    {
        var model = new OrderModel();

        dbcommand.CommandText =
            @"SELECT offers.id, offers.cost_for_day, offers.days, offers.country, offers.address, apartmentstypes.apartments_type_name, apartments.count_rooms, apartments.count_floors, apartments.count_sleeping_places, apartmentsclasses.apartments_class_name FROM offers
	JOIN apartments ON offers.apartments_id = apartments.id
		JOIN apartmentstypes ON apartments.apartments_type_id = apartmentstypes.id
			JOIN apartmentsclasses ON apartments.apartments_class_id = apartmentsclasses.id
				WHERE offers.id = (@p1)";

        var params1 = dbcommand.CreateParameter();

        params1.ParameterName = "p1";
        params1.Value = id;

        dbcommand.Parameters.Add(params1);
        var dataReader = dbcommand.ExecuteReader();


        while (dataReader.Read())
        {
            var tmp = new OfferView();
            tmp.id = (Guid)dataReader.GetValue(0);
            tmp.CostForDay = (double)dataReader.GetValue(1);
            tmp.Days = (int)dataReader.GetValue(2);
            tmp.Country = dataReader.GetValue(3) as string;
            tmp.Address = dataReader.GetValue(4) as string;
            tmp.AppTypeName = dataReader.GetValue(5) as string;
            tmp.CountRooms = (int)dataReader.GetValue(6);
            tmp.CountFloors = (int)dataReader.GetValue(7);
            tmp.CountSleepingPlaces = (int)dataReader.GetValue(8);
            tmp.AppClassName = dataReader.GetValue(9) as string;
            model.Offer = tmp;
        }

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Order(OrderModel model)
    {
        if (ModelState.IsValid)
        {
            var client = GetClient();
            model.DepartureDate = DateOnly.FromDateTime(DateTime.Parse(model.ArrivingDate)).AddDays(model.Offer.Days)
                .ToString();
            model.FinalPrice = model.Offer.CostForDay * model.Offer.Days;


            dbcommand.CommandText =
                @"CALL createOrder((@p1), (@p2), (@p3), cast((@p4) as date), cast((@p5) as date))";
            var params1 = dbcommand.CreateParameter();
            var params2 = dbcommand.CreateParameter();
            var params3 = dbcommand.CreateParameter();
            var params4 = dbcommand.CreateParameter();
            var params5 = dbcommand.CreateParameter();

            params1.ParameterName = "p1";
            params1.Value = client.id;

            params2.ParameterName = "p2";
            params2.Value = model.Offer.id;

            params3.ParameterName = "p3";
            params3.Value = model.FinalPrice;

            params4.ParameterName = "p4";
            params4.Value = model.ArrivingDate;

            params5.ParameterName = "p5";
            params5.Value = model.DepartureDate;

            dbcommand.Parameters.Add(params1);
            dbcommand.Parameters.Add(params2);
            dbcommand.Parameters.Add(params3);
            dbcommand.Parameters.Add(params4);
            dbcommand.Parameters.Add(params5);

            dbcommand.ExecuteReader();
            dbcommand.Parameters.Clear();
            return RedirectToAction("Profile", "Client");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult OrdersList()
    {
        var client = GetClient();
        var model = new OrderListModel();
        model.Orders = new List<OrderModel>();

        dbcommand.CommandText =
            @"SELECT orders.id, offers.cost_for_day, offers.days, offers.country, offers.address, apartmentstypes.apartments_type_name, apartments.count_rooms, apartments.count_floors, apartments.count_sleeping_places, apartmentsclasses.apartments_class_name, orders.final_price, orders.arriving_date, orders.departure_date FROM offers
	JOIN apartments ON offers.apartments_id = apartments.id
		JOIN apartmentstypes ON apartments.apartments_type_id = apartmentstypes.id
			JOIN apartmentsclasses ON apartments.apartments_class_id = apartmentsclasses.id
				JOIN orders ON offers.id = orders.offer_id
					WHERE orders.client_id = (@p1)";

        var params1 = dbcommand.CreateParameter();
        params1.ParameterName = "p1";
        params1.Value = client.id;
        dbcommand.Parameters.Add(params1);

        var dataReader = dbcommand.ExecuteReader();

        while (dataReader.Read())
        {
            var tmp = new OrderModel();
            tmp.Offer = new OfferView();
            tmp.IdToDelete = (Guid?)dataReader.GetValue(0);
            tmp.Offer.CostForDay = (double)dataReader.GetValue(1);
            tmp.Offer.Days = (int)dataReader.GetValue(2);
            tmp.Offer.Country = dataReader.GetValue(3) as string;
            tmp.Offer.Address = dataReader.GetValue(4) as string;
            tmp.Offer.AppTypeName = dataReader.GetValue(5) as string;
            tmp.Offer.CountRooms = (int)dataReader.GetValue(6);
            tmp.Offer.CountFloors = (int)dataReader.GetValue(7);
            tmp.Offer.CountSleepingPlaces = (int)dataReader.GetValue(8);
            tmp.Offer.AppClassName = dataReader.GetValue(9) as string;
            tmp.FinalPrice = (double?)dataReader.GetValue(10);
            tmp.ArrivingDate = DateOnly.FromDateTime((DateTime)dataReader.GetValue(11)).ToString();
            tmp.DepartureDate = DateOnly.FromDateTime((DateTime)dataReader.GetValue(12)).ToString();
            model.Orders!.Add(tmp);
        }

        dataReader.Close();
        dbcommand.Parameters.Clear();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> OrdersList(Guid id)
    {
        if (ModelState.IsValid)
        {
            dbcommand.CommandText = @"DELETE FROM orders WHERE id = (@p1)";
            var params1 = dbcommand.CreateParameter();
            params1.ParameterName = "p1";
            params1.Value = id;
            dbcommand.Parameters.Add(params1);
            dbcommand.ExecuteReader();
            dbcommand.Parameters.Clear();
            return RedirectToAction("Profile", "Client");
        }

        return View();
    }

    [HttpGet]
    public IActionResult AskQuestion()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AskQuestion(QuestionModel model)
    {
        var client = GetClient();

        dbcommand.CommandText = @"INSERT INTO questions VALUES (gen_random_uuid(), (@p1), (@p2),'', false)";

        var params1 = dbcommand.CreateParameter();
        var params2 = dbcommand.CreateParameter();

        params1.ParameterName = "p1";
        params1.Value = client.id;

        params2.ParameterName = "p2";
        params2.Value = model.Question;

        dbcommand.Parameters.Add(params1);
        dbcommand.Parameters.Add(params2);

        dbcommand.ExecuteReader();
        dbcommand.Parameters.Clear();

        return RedirectToAction("Profile", "Client");

        return View();
    }
}