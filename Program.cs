using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Security_CSharp.Data;
using Security_CSharp.Exceptions;
using Security_CSharp.Security.Interfaces;
using Security_CSharp.Security.Repositories;
using Security_CSharp.Security.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// CORS Settings
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://example.com",
                                                  "http://localhost:5173",
                                                  "http://localhost:5174"
                                                  )
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});


builder.Services.AddControllers();

// Using Response Caching
builder.Services.AddResponseCaching();

// Services and Repositories
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();



// Add Custom Exception Handlers
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddProblemDetails();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// Specifying Authentication Schema
var tokenSecret = builder.Configuration.GetSection("AppSettings:TokenSecret").Value ?? throw new Exception("TokenSecret is not set.");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });



// Database
var connectionString = builder.Configuration.GetConnectionString("default") ?? throw new Exception("\"default\" connectionString is not set.");
var serverVersion = ServerVersion.AutoDetect(connectionString);
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(connectionString, serverVersion)
    // Only for development. Delete when deploying.
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{ }
app.UseSwagger();
app.UseSwaggerUI();


// Using Custom ExceptionHandlers
app.UseExceptionHandler();


app.UseHttpsRedirection();

// CORS
app.UseCors(MyAllowSpecificOrigins);


app.UseAuthentication();
app.UseAuthorization();

// Caching
app.UseResponseCaching();

app.MapControllers();


// Run migrations at every start
using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
await dbContext.Database.EnsureDeletedAsync();
await dbContext.Database.MigrateAsync();


app.Run();
