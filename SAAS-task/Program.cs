using Microsoft.EntityFrameworkCore;
using SAAS_task.Context;
using SAAS_task.Filters;
using SAAS_task.User.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// build var for the StringConection
var conectionString = builder.Configuration.GetConnectionString("Connection");

// Register server
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conectionString));

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddScoped<PersonServices>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
