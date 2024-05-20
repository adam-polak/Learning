using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public class PeopleData : PersonModel
    {
        private readonly ISqlDataAccess _db;

        public PeopleData(SqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<PersonModel>> GetPeople()
        {
            string sql = "select * from dbo.People";
        }
    }
}