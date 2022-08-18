using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Wuphf.Api;
using Wuphf.Api.OpenApi;
using Wuphf.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData(
    opt => opt.EnableQueryFeatures().AddRouteComponents("odata", EdmModelBuilder.GetEdmModel()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(_ => _.UseSqlServer(builder.Configuration.GetConnectionString("DbContext")));
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use odata route debug, /$odata
app.UseODataRouteDebug();
// If you want to use /$openapi, enable the middleware.
app.UseODataOpenApi();
// Add OData /$query middleware
app.UseODataQueryRequest();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllers();

app.MapHub<ApiHub>("/hub");

using var scope = app.Services.CreateScope();
await scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.MigrateAsync();

app.Run();
