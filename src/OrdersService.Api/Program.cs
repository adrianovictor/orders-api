using System.Reflection;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using OrdersService.Api.Extensions;
using OrdersService.Application.Commands.Customers.CreateCustomer;
using OrdersService.Application.Notifications;
using OrdersService.Application.Services;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Domain.Interfaces.UoW;
using OrdersService.Infrastructure.Data.Context;
using OrdersService.Infrastructure.Data.Repositories.Reading;
using OrdersService.Infrastructure.Data.Repositories.Writing;
using OrdersService.Infrastructure.Data.Settings;
using OrdersService.Infrastructure.Data.UoW;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(typeof(CreateCustomerHandler).GetTypeInfo().Assembly);
builder.Services.AddDbContext<OrdersDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return new MongoDbContext(settings);
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
builder.Services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

builder.Services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();

builder.Services.AddHostedService<SyncDataHostedService>();
builder.Services.AddTransient<INotificationHandler<SyncDataNotification>, SyncDataHandler>();


builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.WithOrigins("*")
            .AllowAnyOrigin()
            .AllowAnyMethod()
          .AllowAnyHeader();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthCheckExtension(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

#region HEALTH CHECK APP
app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = p => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    AllowCachingResponses = false,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status400BadRequest
    }
});

string cssLocation = builder.Configuration["HealthCheck:Css"]!; //configuration.GetValue<string>("HealthCheck:Css")!;
if (string.IsNullOrEmpty(cssLocation)) throw new ApplicationException("CSS do dashboard do Health Check não esta parametrizado no appsettings");

app.UseHealthChecksUI(options =>
{
    options.UIPath = "/dashboard";
    options.AddCustomStylesheet(cssLocation);
});
#endregion

app.UseCors("AllowAllOrigins");
app.Run();
