using Chatappapi.Helpers;
using Chatappapi.Interface;
using Chatappapi.Model;
using Chatappapi.Repository;
using Chatappapi.services;
using management_system_backend_api.Database.SqlConnectionPlace;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["jwt:Issuer"],
        ValidAudience = builder.Configuration["jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["jwt:Key"]))
    };
});
builder.Services.AddAuthorization();

// Add Repository and Interface to a Containera
builder.Services.AddScoped<IAuthentication,Authentication>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IForgotPasswordRepository, ForgotPasswordRepository>();
builder.Services.AddScoped<IContactsRepository, ContactsRepository>();

//Add Services
builder.Services.AddScoped<ProfileCloudService>();
builder.Services.AddScoped<AuthServices>();
builder.Services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();
//builder.Services.AddTransient<IEmailService, EmailService>();

//Add SqlConnectionPlace
builder.Services.AddScoped<SqlConnectionFactory>();

// Add Helper Service
builder.Services.AddSingleton<AuthSettings>(sp =>
    new AuthSettings(sp.GetRequiredService<IConfiguration>()));
builder.Services.AddSingleton<ProfileHelperService>();

//Add Cors Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("chatAppCorsPolicy",
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("chatAppCorsPolicy");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
