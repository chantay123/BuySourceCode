using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using WebBuySource.Data;
using WebBuySource.Interfaces;
using WebBuySource.Services;
using WebBuySource.Uow;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

//connect db 
//var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DBCon");

//if (string.IsNullOrEmpty(connectionString))
//{
//    throw new Exception("Connection string not found. Ensure the .env file is correctly configured and placed in the root directory.");
//}


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


/// cors
#region
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
#endregion

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




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
