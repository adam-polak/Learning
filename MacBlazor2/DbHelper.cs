using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace MacBlazor2
{
    public class DbHelper
    {
        private IDbConnection connection;

        public DbHelper() {
            connection = new SqlConnection("User ID=testuser;Password=password;Server=localhost:5432;Database=dvdrental;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;");
            var t = connection.Query<Person>("SELECT actor_id, first_name, last_name FROM actor");
            foreach(Person p in t) Console.WriteLine(p.first_name);
        }

        public async void PrintPeople() {
            IEnumerable<Person>? people = null;
            try {
                Console.WriteLine("1");
                people = await connection.QueryAsync<Person>("SELECT actor_id, first_name, last_name FROM actor");
                Console.WriteLine("2");
                foreach(Person p in people) Console.WriteLine(p.first_name);
                Console.WriteLine("3");
            } finally {
                if(people == null) Console.WriteLine("Null");
                else Console.WriteLine("Something else");
            }
        }
    }
}