using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebBuySource.Data;
using WebBuySource.Interfaces;
using WebBuySource.Services;
using WebBuySource.Uow;


var builder = WebApplication.CreateBuilder(args);


Env.Load();
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

//Get CORS from .env
var allowedOriginsEnv = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS")?
    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    ?? new[] { "http://localhost:3000" };

// Get URL from .env
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

//connect db 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

//sign DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService , UserService >();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// memmory cache
builder.Services.AddMemoryCache();

//Get value form .env
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ;
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ;
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

// Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();

///// cors
//#region
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("MyCorsPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS")
              .WithHeaders("Authorization", "Content-Type", "X-Request-Id")
              .WithExposedHeaders("X-Pagination")
              .DisallowCredentials();
    });
});
//#endregion

builder.Services.AddAuthorization();
builder.Services.AddControllers();


var app = builder.Build();
app.UseCors("MyCorsPolicy");

///setup Middleware username password swagger
#region
app.UseMiddleware<SwaggerBasicAuthMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
#endregion

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseHsts(); // HTTP Strict Transport Security

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
