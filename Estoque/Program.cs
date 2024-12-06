using Microsoft.EntityFrameworkCore;
using Estoque.Data;
using Swashbuckle.AspNetCore.SwaggerGen; // Adicionada diretiva para Swagger

var builder = WebApplication.CreateBuilder(args);

// Configurar banco de dados SQLite
builder.Services.AddDbContext<EstoqueDbContext>(options =>
    options.UseSqlite("Data Source=estoque.db"));

// Configuração de controladores
builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Certifique-se de que esse método está presente

// Configurar HttpClient para comunicação com o microsserviço de Autenticação
builder.Services.AddHttpClient("AutenticacaoAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5139"); // URL do microsserviço de Autenticação
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

// Configuração do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Esse método deve estar disponível
    app.UseSwaggerUI(); // Esse método deve estar disponível
}

app.UseAuthorization();
app.MapControllers();

// Rota adicional de boas-vindas
app.MapGet("/", () => "Bem-vindo ao Microsserviço de Estoque!");

app.Run();
