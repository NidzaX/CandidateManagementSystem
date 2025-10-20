using CandidateManagementSystem.Api.Extensions;
using CandidateManagementSystem.Application;
using CandidateManagementSystem.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<DataSeeder>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.ApplyMigrations();

    using (IServiceScope scope = app.Services.CreateScope())
    {
        DataSeeder seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        await seeder.SeedAsync();
    }
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();