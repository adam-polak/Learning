using webAPI.models;
using Dapper;
using Npgsql;

namespace webAPI.controllers;

public static class testDbController
{
    private static string connectionString = "Host=localhost:5432;Username=postgres;Password=password;Database=apidb;";

    public static Person? GetPerson(int id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        List<Person> people = (List<Person>)connection.Query<Person>($"SELECT * FROM person_table WHERE id={id};");
        Person? person = people.First();
        return person;
    }

    public static List<Person> GetAllPeople()
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        return (List<Person>)connection.Query<Person>("SELECT * FROM person_table;");
    }
}