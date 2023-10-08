using Events.Persistence;
using Map;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Module.Application.Interfaces;
using Module.Persistence;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IDbContext>(provider => provider.GetRequiredService<ModuleDbContext>());
//builder.Services.AddScoped<IEventDbContext>(provider => provider.GetRequiredService<EventsDbContext>());

builder.Services.AddDbContext<ModuleDbContext>((serviceProvider, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");

    var serverVersion = new MySqlServerVersion(new Version(10, 5, 19));

    options.UseMySql(connectionString, serverVersion);
});

/*
builder.Services.AddDbContext<EventsDbContext>((serviceProvider, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");

    var serverVersion = new MySqlServerVersion(new Version(10, 5, 19));

    options.UseMySql(connectionString, serverVersion);
});
*/
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(Module.AssemblyReference))));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(Map.AssemblyReference))));

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ClockSkew = TimeSpan.FromMinutes(30)
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();