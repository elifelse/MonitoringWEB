using Microsoft.EntityFrameworkCore;
using MonitoringApp.Persistence.Contexts;
using MonitoringApp.API.Services;

var builder = WebApplication.CreateBuilder(args);

// **HttpClient desteği ekleniyor!**
builder.Services.AddHttpClient();

// **DbContext ekleniyor**
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MonitoringDbContext>(options =>
    options.UseNpgsql(connectionString));

// **Background Service ekleniyor**
builder.Services.AddHostedService<HealthCheckService>();

// **Controllerlar ve Swagger ekleniyor**
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
