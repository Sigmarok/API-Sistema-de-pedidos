using Microsoft.EntityFrameworkCore;
using SistemaPedidosAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container de dependências.
builder.Services.AddControllers();

// Configuração de conexão com o PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
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

// Mapeamento de endpoints dos controladores
app.MapControllers();

app.Run();

