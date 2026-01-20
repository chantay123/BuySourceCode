using DotNetEnv;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
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
builder.Services.AddHttpClient();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService , UserService >();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddTransient<ICodeService, CodeService>();
builder.Services.AddTransient<ICodeFileService, CodeFileService>();
builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddTransient<IProgrammingLanguageService, ProgrammingLanguageService>();
builder.Services.AddTransient<ICommentService, CommentService>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services
builder.Services.AddControllers();

// memmory cache
builder.Services.AddMemoryCache();

//Get value form .env
var jwtAccessKey = Environment.GetEnvironmentVariable("JWT_ACCESS_KEY");
var jwtRefreshKey = Environment.GetEnvironmentVariable("JWT_REFRESH_KEY");
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ;
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

// Add JWT Authentication
var clientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
// COOKIE temp Google OAuth
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
// GOOGLE LOGIN
.AddGoogle("Google", options =>
{
    options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID")!;
    options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET")!;
    options.CallbackPath = "/api/v1/auth/google-callback";
})

// JWT cho API
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtAccessKey!)
        ),
        RoleClaimType = ClaimTypes.Role,
        ClockSkew = TimeSpan.Zero
    };
});

//
var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");
var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");


// Config Cloudinary service
//builder.Services.AddSingleton(new CloudinaryService(cloudName, apiKey, apiSecret));
builder.Services.AddAuthorization();


// Add Swagger and JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebBuySource API",
        Version = "v1"
    });

    // Add JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT token. Example: Bearer "
    });

    //  Require JWT globally (optional)
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

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

app.UseForwardedHeaders();

app.MapControllers();

app.Run();
