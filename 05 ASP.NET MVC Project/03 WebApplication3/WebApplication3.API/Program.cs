using Microsoft.EntityFrameworkCore;
using WebApplication3.Application.Services;
using WebApplication3.Application.Services.Base;
using WebApplication3.Infrastructure.Data;
using WebApplication3.Infrastructure.Repositories;
using WebApplication3.Infrastructure.Repositories.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISecurityIncidentRepository, SecurityIncidentRepository>();
builder.Services.AddScoped<ISecurityOfficerRepository, SecurityOfficerRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ISecurityIncidentService, SecurityIncidentService>();
builder.Services.AddScoped<ISecurityOfficerService, SecurityOfficerService>();

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