using Dapper;
using Microsoft.EntityFrameworkCore;
using ApiEntegrasyon.Models;

namespace ApiEntegrasyon.Context
{
    public class AIDbContext : DbContext
    {
        public AIDbContext()
        {
        }

        public AIDbContext(DbContextOptions<DbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Connect.ConnectionString);
            }
        }

        public int Execute(string query, object parameters)
        {
            return Database.GetDbConnection().Execute(query, parameters);
        }

        public List<T> SQLQuery<T>(string query, object parameters)
        {
            return Database.GetDbConnection().Query<T>(query, param: parameters).ToList();
        }

        public List<T> SQLQuery<T>(string query)
        {
            return Database.GetDbConnection().Query<T>(query).ToList();
        }

    }
}
