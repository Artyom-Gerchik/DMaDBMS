using LAB6.Entities;
using LAB6.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace LAB6.Controllers;

public class AdminController : Controller
{
    private readonly NpgsqlCommand dbcommand;

    public AdminController()
    {
        dbcommand = DB_Manager.getCommand();
    }

    private AdminProfileModel GetAdmin()
    {
        var model = new AdminProfileModel();

        dbcommand.CommandText =
            @"SELECT users.id, users.login FROM users JOIN administrators ON users.login = (@p1) AND users.id = administrators.id;";

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
        return View(GetAdmin());
    }

    [HttpGet]
    public IActionResult AddOffer()
    {
        var model = new AddOfferModel();
        model.Apartments = new List<Apartments?>();

        dbcommand.CommandText =
            @"SELECT apartments.id, apartmentstypes.apartments_type_name, apartments.count_rooms, apartments.count_floors, apartments.count_sleeping_places, apartmentsclasses.apartments_class_name FROM apartments
	JOIN apartmentstypes ON apartments.apartments_type_id = apartmentstypes.id
		JOIN apartmentsclasses ON apartments.apartments_class_id = apartmentsclasses.id";

        var dataReader = dbcommand.ExecuteReader();
        while (dataReader.Read())
        {
            var tmp = new Apartments();

            tmp.id = (Guid)dataReader.GetValue(0);
            tmp.AppTypeName = dataReader.GetValue(1) as string;
            tmp.CountRooms = (int)dataReader.GetValue(2);
            tmp.CountFloors = (int)dataReader.GetValue(3);
            tmp.CountSleepingPlaces = (int)dataReader.GetValue(4);
            tmp.AppClassName = dataReader.GetValue(5) as string;

            model.Apartments.Add(tmp);
        }

        dataReader.Close();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddOffer(AddOfferModel model)
    {
        if (ModelState.IsValid)
        {
            dbcommand.CommandText = @"INSERT INTO offers VALUES (gen_random_uuid(),
						   (@p1),
						   (@p2),
						   (@p3),
						   (@p4),
						   (@p5));";

            var params1 = dbcommand.CreateParameter();
            var params2 = dbcommand.CreateParameter();
            var params3 = dbcommand.CreateParameter();
            var params4 = dbcommand.CreateParameter();
            var params5 = dbcommand.CreateParameter();

            params1.ParameterName = "p1";
            params1.Value = model.idOfSelectedApp;

            params2.ParameterName = "p2";
            params2.Value = model.CostForDay;

            params3.ParameterName = "p3";
            params3.Value = model.Days;

            params4.ParameterName = "p4";
            params4.Value = model.Country;

            params5.ParameterName = "p5";
            params5.Value = model.Address;

            dbcommand.Parameters.Add(params1);
            dbcommand.Parameters.Add(params2);
            dbcommand.Parameters.Add(params3);
            dbcommand.Parameters.Add(params4);
            dbcommand.Parameters.Add(params5);

            dbcommand.ExecuteReader();
            dbcommand.Parameters.Clear();

            return RedirectToAction("Profile", "Admin");
        }

        return View(model);
    }

    private OffersListModel GetAllOffers()
    {
        var model = new OffersListModel();
        model.Offers = new List<OfferView?>();

        dbcommand.CommandText =
            @"SELECT offers.id, offers.cost_for_day, offers.days, offers.country, offers.address, apartmentstypes.apartments_type_name, apartments.count_rooms, apartments.count_floors, apartments.count_sleeping_places, apartmentsclasses.apartments_class_name FROM offers
	JOIN apartments ON offers.apartments_id = apartments.id
		JOIN apartmentstypes ON apartments.apartments_type_id = apartmentstypes.id
			JOIN apartmentsclasses ON apartments.apartments_class_id = apartmentsclasses.id";

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
            model.Offers.Add(tmp);
        }

        dataReader.Close();
        return model;
    }

    [HttpGet]
    public IActionResult OffersList()
    {
        return View(GetAllOffers());
    }

    [HttpPost]
    public IActionResult DeleteOffer(Guid id)
    {
        dbcommand.CommandText = @"DELETE FROM offers WHERE id = (@p1)";

        var params1 = dbcommand.CreateParameter();

        params1.ParameterName = "p1";
        params1.Value = id;

        dbcommand.Parameters.Add(params1);

        dbcommand.ExecuteReader();
        dbcommand.Parameters.Clear();

        return RedirectToAction("OffersList", "Admin");
    }

    [HttpGet]
    public IActionResult EditOffer(Guid id)
    {
        var model = new AddOfferModel();

        model.Apartments = new List<Apartments?>();

        dbcommand.CommandText =
            @"SELECT apartments.id, apartmentstypes.apartments_type_name, apartments.count_rooms, apartments.count_floors, apartments.count_sleeping_places, apartmentsclasses.apartments_class_name FROM apartments
	JOIN apartmentstypes ON apartments.apartments_type_id = apartmentstypes.id
		JOIN apartmentsclasses ON apartments.apartments_class_id = apartmentsclasses.id";

        var dataReader = dbcommand.ExecuteReader();
        while (dataReader.Read())
        {
            var tmp = new Apartments();

            tmp.id = (Guid)dataReader.GetValue(0);
            tmp.AppTypeName = dataReader.GetValue(1) as string;
            tmp.CountRooms = (int)dataReader.GetValue(2);
            tmp.CountFloors = (int)dataReader.GetValue(3);
            tmp.CountSleepingPlaces = (int)dataReader.GetValue(4);
            tmp.AppClassName = dataReader.GetValue(5) as string;

            model.Apartments.Add(tmp);
        }

        dataReader.Close();

        dbcommand.CommandText = @"SELECT * FROM offers WHERE id = (@p1)";

        var params1 = dbcommand.CreateParameter();

        params1.ParameterName = "p1";
        params1.Value = id;

        dbcommand.Parameters.Add(params1);

        dataReader = dbcommand.ExecuteReader();
        while (dataReader.Read())
        {
            model.idOfSelectedApp = (Guid)dataReader.GetValue(1);
            model.CostForDay = (double)dataReader.GetValue(2);
            model.Days = (int)dataReader.GetValue(3);
            model.Country = dataReader.GetValue(4) as string;
            model.Address = dataReader.GetValue(5) as string;
        }

        model.idForEdit = id;

        dbcommand.Parameters.Clear();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditOffer(AddOfferModel model)
    {
        if (ModelState.IsValid)
        {
            dbcommand.CommandText =
                @"UPDATE offers SET apartments_id = (@p1), cost_for_day = (@p2), days = (@p3), country = (@p4), address = (@p5) WHERE id = (@p6)";

            var params1 = dbcommand.CreateParameter();
            var params2 = dbcommand.CreateParameter();
            var params3 = dbcommand.CreateParameter();
            var params4 = dbcommand.CreateParameter();
            var params5 = dbcommand.CreateParameter();
            var params6 = dbcommand.CreateParameter();

            params1.ParameterName = "p1";
            params1.Value = model.idOfSelectedApp;

            params2.ParameterName = "p2";
            params2.Value = model.CostForDay;

            params3.ParameterName = "p3";
            params3.Value = model.Days;

            params4.ParameterName = "p4";
            params4.Value = model.Country;

            params5.ParameterName = "p5";
            params5.Value = model.Address;

            params6.ParameterName = "p6";
            params6.Value = model.idForEdit;

            dbcommand.Parameters.Add(params1);
            dbcommand.Parameters.Add(params2);
            dbcommand.Parameters.Add(params3);
            dbcommand.Parameters.Add(params4);
            dbcommand.Parameters.Add(params5);
            dbcommand.Parameters.Add(params6);

            dbcommand.ExecuteReader();
            dbcommand.Parameters.Clear();
            return RedirectToAction("OffersList", "Admin");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Logs()
    {
        var model = new LogModel();
        model.logs = new List<Log>();

        dbcommand.CommandText =
            @"SELECT logs.id, logs.client_id , logs.happened_at, logstypes.log_type_name FROM logs
                JOIN logstypes ON logs.log_type_id = logstypes.id;";

        var dataReader = dbcommand.ExecuteReader();
        while (dataReader.Read())
        {
            var tmp = new Log();
            tmp.id = (Guid)dataReader.GetValue(0);
            tmp.clientId = (Guid)dataReader.GetValue(1);
            tmp.happenedAt = (DateTime)dataReader.GetValue(2);
            tmp.logType = dataReader.GetValue(3) as string;
            model.logs.Add(tmp);
        }

        dataReader.Close();

        return View(model);
    }
}