using dotnet6_jwt_auth.Helpers;
using dotnet6_jwt_auth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add services to DI container
{
  var services = builder.Services;
  services.AddCors();
  services.AddControllers();

  // configure strongly typed settings object
  services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

  // configure DI for application services
  services.AddScoped<IUserService, UserService>();
}

var app = builder.Build();

// configure HTTP request pipeline
{
  // global cors policy
  app.UseCors(x => x
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());

  // custom jwt auth middleware
  app.UseMiddleware<JwtMiddleware>();

  app.MapControllers();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// app.MapControllers();

app.Run("http://localhost:3000");
