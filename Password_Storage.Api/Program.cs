using Npgsql;
using Microsoft.EntityFrameworkCore;
using Password_Storage.Api.Context;
using Password_Storage.Api.Services.IServices;
using Password_Storage.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IPasswordService, PasswordService>();

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<PasswordDbContext>(options =>
    options.UseNpgsql(connectionString));




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
