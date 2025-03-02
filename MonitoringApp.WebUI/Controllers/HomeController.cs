using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using MonitoringApp.WebUI.Models;

namespace MonitoringApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var logsResponse = await _httpClient.GetStringAsync("http://localhost:5136/api/ServiceHealthLogs");
                var logs = JsonSerializer.Deserialize<List<ServiceHealthLog>>(logsResponse, new JsonSerializerOptions { 
                    PropertyNameCaseInsensitive = true, 
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull 
                });

                var errorsResponse = await _httpClient.GetStringAsync("http://localhost:5136/api/Errors");
                var errors = JsonSerializer.Deserialize<List<ErrorLog>>(errorsResponse, new JsonSerializerOptions { 
                    PropertyNameCaseInsensitive = true 
                });

                var jobsResponse = await _httpClient.GetStringAsync("http://localhost:5136/api/Jobs");
                var jobs = JsonSerializer.Deserialize<List<JobStatus>>(jobsResponse, new JsonSerializerOptions { 
                    PropertyNameCaseInsensitive = true 
                });

                var viewModel = new DashboardViewModel
                {
                    ServiceHealthLogs = logs,
                    Errors = errors,
                    Jobs = jobs
                };

                return View(viewModel);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.ErrorMessage = "API'ye bağlanılamadı. Lütfen API'nin çalıştığından emin olun.";
                return View(new DashboardViewModel());
            }
        }

    }
}
