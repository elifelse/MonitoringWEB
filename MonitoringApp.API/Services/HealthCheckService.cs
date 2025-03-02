using MonitoringApp.Persistence.Contexts;
using MonitoringApp.Persistence.Entities;

namespace MonitoringApp.API.Services
{
    public class HealthCheckService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly HttpClient _httpClient;

        public HealthCheckService(IServiceScopeFactory scopeFactory, IHttpClientFactory httpClientFactory)
        {
            _scopeFactory = scopeFactory;
            _httpClient = httpClientFactory.CreateClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<MonitoringDbContext>();
                    var applications = context.Applications.ToList();

                    foreach (var app in applications)
                    {
                        var healthStatus = "Unhealthy";
                        var responseTime = 0;
                        var errorMessage = "";
                        var statusCode = 500;

                        try
                        {
                            var startTime = DateTime.UtcNow;
                            var response = await _httpClient.GetAsync(app.Url, stoppingToken);
                            responseTime = (int)(DateTime.UtcNow - startTime).TotalMilliseconds;

                            if (response.IsSuccessStatusCode)
                            {
                                healthStatus = "Healthy";
                                statusCode = (int)response.StatusCode;
                            }
                            else
                            {
                                healthStatus = "Unhealthy";
                                statusCode = (int)response.StatusCode;
                                errorMessage = $"HTTP {statusCode}";
                            }
                        }
                        catch (Exception ex)
                        {
                            errorMessage = ex.Message;
                        }

                        // Veritabanına sağlık durumunu yaz
                        var log = new Servicehealthlogs
                        {
                            Applicationid = app.Id,
                            Status = healthStatus,
                            Responsetimems = responseTime,
                            Errormessage = errorMessage,
                            Statuscode = statusCode,
                            Checkedat = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                            Source = "Auto"
                        };


                        context.Servicehealthlogs.Add(log);
                        await context.SaveChangesAsync();
                    }
                }

                // 5 dakika bekle
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
