using Microsoft.EntityFrameworkCore;
using Pagamentos.Data;

var builder = WebApplication.CreateBuilder(args);

// Adicionar DbContext e dependências
builder.Services.AddDbContext<PagamentoDbContext>(options =>
    options.UseSqlite("Data Source=pagamentos.db"));

// Adicionar suporte a controllers
builder.Services.AddControllers();

// Configurar o HTTP Client para o microserviço de Autenticação
builder.Services.AddHttpClient("AutenticacaoAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5139"); // Ajuste para o URL correto do microserviço de autenticação
});

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ativar Swagger apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Mapear os controllers
app.MapControllers();

app.Run();
