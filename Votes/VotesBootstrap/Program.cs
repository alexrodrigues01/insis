using VotesBootstrap.Helpers;
using VotesBootstrap.Interfaces.Repository;
using VotesBootstrap.Interfaces.Service;
using VotesBootstrap.Model;
using VotesBootstrap.Repositories;
using VotesBootstrap.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<MongoDatabaseInfoSettings>(
    builder.Configuration.GetSection("MongoDataBase"));
builder.Services.AddHostedService<RabbitMQConsumer>();


builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();

//builder.Services.AddSingleton<MessageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
