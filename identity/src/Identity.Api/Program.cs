using FluentValidation.AspNetCore;
using Identity.Api.Filters;
using Identity_Application;
using Identity_Infrastructure;
using Identity_Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<DbMigration>();
    await initialiser.InitialiseAsync();
}
var applicationDbContextInitializer = app.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
await applicationDbContextInitializer.SeedAsync();

app.UseCors("CORS_POLICY");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
