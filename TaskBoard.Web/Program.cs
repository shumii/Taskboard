using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Application.Services;
using TaskBoard.Data.Context;
using TaskBoard.Domain.Model;
using TaskBoard.Web;
using TaskBoard.Web.DTO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

DependencyRegistrations.RegisterDependencies(builder.Services);

builder.Services.AddDbContext<TaskBoardDbContext>(options =>
    options.UseInMemoryDatabase("TaskBoardDb"));

var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "TaskBoard API v1");
    });
}

Endpoints.AddEndpoints(app);
Seeder.SeedDataForDemo(app);

app.Run();