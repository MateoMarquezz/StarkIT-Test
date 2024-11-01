using App.Services;
using App.DataAccess; 
using Microsoft.AspNetCore.Builder;
using App.DataAccess;
using App.Services;

var builder = WebApplication.CreateBuilder(args);


string jsonFilePath = builder.Configuration.GetSection("Database:JsonFilePath").Value;
string logFilePath = builder.Configuration.GetSection("Logging:FilePath").Value;

builder.Logging.ClearProviders();
builder.Logging.AddProvider(new JsonFileLoggerProvider(logFilePath)); 

builder.Services.AddControllers();
builder.Services.AddScoped<UserRepository>(provider => new UserRepository(jsonFilePath));
builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
