using ApiOAuthVideoJuegos.Data;
using ApiOAuthVideoJuegos.Helpers;
using ApiOAuthVideoJuegos.Models;
using ApiOAuthVideoJuegos.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString =
    builder.Configuration.GetConnectionString("AzureServer");
builder.Services.AddSingleton<HelperOAuthToken>();
HelperOAuthToken helper = new HelperOAuthToken(builder.Configuration);
//AÑADIR LAS OPCIONES DE AUTENTICACION
builder.Services.AddAuthentication(helper.GetAuthenticationOptions()).AddJwtBearer(helper.GetJwtOptions());
builder.Services.AddTransient<RepositoryVideoJuegos>();
builder.Services.AddTransient<RepositoryUsuariosGaming>();
builder.Services.AddDbContext<VideoJuegosContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Apii OAuth Videojuegos 2023",
        Version = "v1",
        Description = "Api Videojuegos con seguridad token"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Api Crud Videojuegos");
    options.RoutePrefix = "";
});
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
