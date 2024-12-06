using Microsoft.EntityFrameworkCore;
using Autenticacao.Data; // Namespace do contexto do banco de dados

var builder = WebApplication.CreateBuilder(args);

// Configurando o banco de dados SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=autenticacao.db"));

// Configuração de controladores
builder.Services.AddControllers();

// Configuração do pipeline OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurando a documentação Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuração para autorização (pode ser expandida conforme necessário)
app.UseAuthorization();

// Mapeando os controladores
app.MapControllers();

// Rota adicional para previsão do tempo (exemplo)
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Adicionando a rota principal de boas-vindas
app.MapGet("/", () => "Bem-vindo ao Microsserviço de Autenticação!");

app.Run();

// Record para exemplo de previsão do tempo
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
