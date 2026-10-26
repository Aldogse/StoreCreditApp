using System.Security.Claims;
using System.Text;
using CreditApplication.Database;
using CreditApplication.Interfaces;
using CreditApplication.Repository;
using CreditApplication.Seeder;
using CreditApplication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.Users;
using Serilog;
using Microsoft.OpenApi.Models;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    .WriteTo.File
    (
      path: "Logs/log-.txt",
      rollingInterval: RollingInterval.Day,
      retainedFileCountLimit: 30,
      outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message:lj}{NewLine}{Exception}"
    ).CreateLogger();

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddSwaggerGen(options =>
 {
     options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
     {
         Name = "Authorization",
         Type = SecuritySchemeType.Http,
         BearerFormat = "JWT",
         In = ParameterLocation.Header,
         Description = "Enter JWT token",
     });

     options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//DATABASE CONNECTIONS
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});


builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<ICreditRepository,CreditRepository>();

builder.Services.AddIdentity<LoginCredentials,IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
     .AddDefaultTokenProviders();
    

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Token"])),
            RoleClaimType = ClaimTypes.Role
        };
    });

builder.Services.AddControllers();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roles = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleSeeder.SeedRoleAsync(roles);
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI(); 
    }


 
app.UseExceptionHandler(error =>
{
    error.Run(async (context) =>
    {
        var exceptionFeatures = context.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionFeatures?.Error;

        if(exception != null)
        {
            Log.Error(exception,"Unhandled exception!");
        }

        context.Response.StatusCode = 500;
    });
});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors();



app.Run();

