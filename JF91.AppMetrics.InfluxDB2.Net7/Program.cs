using JF91.AppMetricsInfluxDB2.Middleware;
using JF91.AppMetricsInfluxDB2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AppMetrics Step 1:
// Add AppMetrics and MVC Services
builder.Services.AddMetricsServices();

// AppMetrics Step 2:
// Configure InfluxDB2 App.Metrics Reporter
builder.WebHost.AddInfluxDb2AppMetrics(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

// AppMetrics Step 3:
// Add AppMetrics Middlewares
app.UseRequestsCounterMiddleware();
app.UseRequestsDurationMiddleware();
app.UseResponsesSizeMiddleware();
app.UseResponsesApdexMiddleware();
app.UseMetricsAllMiddleware();
app.UseMetricsAllEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// AppMetrics Step 4:
// Add AppMetrics Global Status Code / Exception Middleware
app.UseHttpStatusCodesCounterMiddleware();

app.Run();