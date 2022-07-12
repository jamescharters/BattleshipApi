using BattleshipApi.Core.Factories;
using BattleshipApi.Core.Interfaces;
using BattleshipApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });

// START Dependencies

// In the real world, a singleton in-memory repository like this is not recommended (implementation not thread safe)
builder.Services.AddSingleton<IGameRepository, GameRepository>();

builder.Services.AddSingleton<IGameFactory, GameFactory>();
builder.Services.AddSingleton<IPlayerFactory, PlayerFactory>();
builder.Services.AddSingleton<IVesselFactory, VesselFactory>();

// END Dependencies


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