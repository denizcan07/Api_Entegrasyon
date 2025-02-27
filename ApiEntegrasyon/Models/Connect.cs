namespace ApiEntegrasyon.Models
{
    public class Connect

    {

        private static string _connectionString = "";

        public static string ConnectionString { get { return _connectionString; } }




        public static void setConnectionString(string connectionString)

        {

            _connectionString = connectionString;




        }

    }
}
