using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Data;
using SampleWebAPI.Data.DAL;
using SampleWebAPI.Helpers;
using SampleWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
//.AddJsonOptions(x=>x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//menambahkan konfigurasi EF
builder.Services.AddDbContext<SamuraiContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("SamuraiConnection")).EnableSensitiveDataLogging());

//menambahkan automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//inject class DAL
builder.Services.AddScoped<ISamurai, SamuraiDAL>();
builder.Services.AddScoped<IQuote, QuotesDAL>();
builder.Services.AddScoped<ISword, SwordDAL>();
builder.Services.AddScoped<IElement, ElementDAL>();


// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

//app.UseHttpsRedirection();
//app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
