namespace DataAccessLibrary
{
    public interface ISqlDataAccess
    {
        string ConnectionStringName { get; set; }
        public Task<List<T>> LoadData<T, U>(string sql, U parameters);
    }
}