using Chatappapi.Interface;
using Chatappapi.Model;
using Chatappapi.Repository;
using management_system_backend_api.Database.SqlConnectionPlace;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Repository and Interface to a Containera
builder.Services.AddScoped<IAuthentication,Authencation>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

//Add SqlConnectionPlace
builder.Services.AddScoped<SqlConnectionFactory>();


//Add Cors Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("chatAppCorsPolicy",
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:64713")
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

app.UseAuthorization();

app.MapControllers();

app.Run();
