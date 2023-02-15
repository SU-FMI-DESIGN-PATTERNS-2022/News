using Microsoft.EntityFrameworkCore;
using News.Repository;
using News.Repository.Context;
using News.Repository.Contracts;
using News.Worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<NewsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("News"));
});

builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddHostedService<NewsFetcher>();
builder.Services.AddHostedService<NewsDeleter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else //Added this else clause to be able to access the swagger UI even through docker images
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
