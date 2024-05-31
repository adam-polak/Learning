using System.Data;
using Dapper;
using Npgsql;

namespace MacBlazor2
{
    public class DbHelper
    {
        private IDbConnection connection;

        public DbHelper() {
            connection = new NpgsqlConnection("Host=localhost:5432;Username=postgres;Password=password;Database=dvdrental");
            Console.WriteLine(connection.ConnectionString);
            foreach(Person p in connection.Query<Person>("SELECT actor_id, first_name, last_name FROM actor")) Console.WriteLine(p.first_name + " " + p.last_name);
        }

    }
}