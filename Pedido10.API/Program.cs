using Pedido10.API.Configuration;
using Pedido10.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; //respeitar os nomes explicitamente dos dtos no swagger
    });

// CONFIGURAÇÃO DE JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["jwt:issuer"],
            ValidAudience = builder.Configuration["jwt:audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]))
        };
    });

// CONFIGURE AUTHORIZATION
builder.Services.AddAuthorization();

// dependency injection
builder.Services.RegisterConfig();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

// cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// connection string
Utils.StringConnection = builder.Configuration.GetConnectionString("DefaultConnection");

//log de erros
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// BUILD THE APPLICATION
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// enable cors
app.UseCors("AllowAny");

app.Use(async (context, next) =>
{
    var token = context.Request.Headers["Authorization"];
    Console.WriteLine($"Token recebido: {token}");
    await next();
});

// AUTHENTICATION
app.UseAuthentication(); // FIRST THAN AUTHORIZATION
// AUTHORIZATION
app.UseAuthorization();


app.MapControllers();

app.Run();
