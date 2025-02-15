using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TaskManagement.API.Data;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add EF Core with SQL Server
builder.Services.AddDbContext<TaskDbContext>(options =>
        
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Register the services
builder.Services.AddScoped<ITaskService, TaskService>();   
builder.Services.AddScoped<IUserService, UserService>();

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

app.Run();
