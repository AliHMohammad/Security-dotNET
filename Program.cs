using Microsoft.EntityFrameworkCore;
using Security_CSharp.Data;
using Security_CSharp.Exceptions;
using Security_CSharp.Security.Interfaces;
using Security_CSharp.Security.Repositories;
using Security_CSharp.Security.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

// Tilføj Custom Exceptionhandlers
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddProblemDetails();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Database opsætning
var connectionString = builder.Configuration.GetConnectionString("default");
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<DataContext>(options =>
{
    // Vi giver connectionString som argument
    options.UseMySql(connectionString, serverVersion)
    // Nedenstående kun til dev. Slet ved deployment
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Vi bruger global exceptionhandlers
app.UseExceptionHandler();

// Kør dine migrations ved opstart af api-server
using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
await dbContext.Database.EnsureDeletedAsync();
await dbContext.Database.MigrateAsync();


app.Run();
