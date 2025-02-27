using Hangfire;
using Microsoft.AspNetCore.Mvc;
using ApiEntegrasyon.Models;
using ApiEntegrasyon.Service;

namespace ApiEntegrasyon.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        IConfiguration _configuration;
        string path;
        string connectionString;
        private IntegrationService _integrationService;
        public ApiController(IConfiguration configuration, IntegrationService IntegrationService)
        {
            _configuration = configuration;
            path = _configuration.GetValue<string>("LogPath");
            connectionString = _configuration.GetConnectionString("SednaDB");
            _integrationService = IntegrationService;
        }

        [HttpGet]
        public IActionResult CreateJob(string cron)
        {
            RecurringJob.AddOrUpdate("UpdateApi", () => ExecuteJob(), cron);
            return Ok("");
        }

        public void ExecuteJob()
        {
            try
            {
                int basarilikayit = 0;
                int basarisizkayit = 0;
                System.IO.File.AppendAllText(path + "\\api.txt", "***************\nJob Started:" + DateTime.Now.ToString() + "\r\n");

                //Do work here

                List<IntegrationDto> List = _integrationService.GetList();
                if (List.Count > 0)
                {
                    foreach (var oRow in List)
                    {
                        var client = new HttpClient();
                        string requestUri = "api - url";
                        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                        var response = client.SendAsync(request);
                        HttpResponseMessage responses = response.GetAwaiter().GetResult();
                        responses.EnsureSuccessStatusCode();

                        if (responses.IsSuccessStatusCode)
                        {
                            basarilikayit++;
                        }
                        else
                        {
                            basarisizkayit++;
                        }
                    }
                }

                System.IO.File.AppendAllText(path + "\\api.txt", "Job Aktarma işlemi tamamlandı.\n" + "Başarılı İşlem :" + Convert.ToString(basarilikayit) + "\n" + "Başarısız İşlem :" + Convert.ToString(basarisizkayit) + "\n");
                System.IO.File.AppendAllText(path + "\\api.txt", "Job Succeeded:" + DateTime.Now.ToString() + "\r\n");
            }
            catch (Exception exc)
            {
                System.IO.File.AppendAllText(path + "\\api.txt", "Job Failed:" + DateTime.Now.ToString() + ":******EXEPTION*******" + exc.Message + "\r\n*******STACKTRACE********" + exc.StackTrace + "\r\n");
            }
        }
    }
}

