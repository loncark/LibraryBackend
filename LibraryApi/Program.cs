//using LibraryAPI;
//using LibraryAPI.Repository;
//using LibraryAPI.Service;
//using Microsoft.Extensions.Configuration;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddDbContext<LibraryDbContext>();
//builder.Services.AddScoped<BookRepository>();
//builder.Services.AddScoped<BookService>();
//builder.Services.AddScoped<AuthorRepository>();
//builder.Services.AddScoped<AuthorService>();

builder.Services.AddControllers();
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
