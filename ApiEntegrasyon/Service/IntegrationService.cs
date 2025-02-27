using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using ApiEntegrasyon.Context;
using ApiEntegrasyon.Models;

namespace ApiEntegrasyon.Service
{
    public class IntegrationService
    {

        public List<IntegrationDto> GetList()
        {
            List<IntegrationDto> allList = new List<IntegrationDto>();
            using (var db = new AIDbContext())
            {
                string TableQuery = "select * from ListTable";

                List<string> tableList = db.SQLQuery<string>(TableQuery).ToList();
                if (tableList.Count > 0)
                {
                    foreach (var item in tableList)
                    {
                        string query = $"EXEC dbo.IntegrationList @param1,@param2";
                        try
                        {
                            string cop = Convert.ToString(item.ToString());
                            DateTime dt = Convert.ToDateTime(DateTime.Now.AddDays(-2));

                            var _param = new SqlParameter[]
                            {
                               new  SqlParameter("param1", cop),
                               new  SqlParameter("param2", dt),
                            };
                            allList = db.SQLQuery<IntegrationDto>(query, new { IsyeriKodu = cop, GirisTarihi = dt }).ToList();
                            if (allList.Count > 0)
                            {
                                foreach (var items in allList)
                                {
                                    allList.Add(items);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                        }

                    }
                }

            }
            return allList;
        }
    }
}
