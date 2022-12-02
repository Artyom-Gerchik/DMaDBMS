using System.Data;

namespace LAB6.Entities;

using Npgsql;

public class DB_Manager
{
    public static NpgsqlCommand getCommand()
    {
        NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Port=5432; Database=booking; Username=lnxd; Password=123;");
        connection.Open();
        NpgsqlCommand command = new NpgsqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.Text;
        return command;
    }
}