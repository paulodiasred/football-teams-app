using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FootballTeamsApp.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionando HttpClient para injeção de dependência, permitindo chamadas HTTP para APIs externas
builder.Services.AddHttpClient();

// Registrando o FootballController para que ele possa ser injetado onde necessário
builder.Services.AddScoped<FootballController>();

// Configuração do DbContext com o SQL Server, usando a string de conexão definida no arquivo de configuração
builder.Services.AddDbContext<FootballDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FootballDB")));

// Registra o DataUpdateService como um serviço em segundo plano (BackgroundService) para atualizações automáticas
builder.Services.AddHostedService<DataUpdateService>();

// Adiciona suporte para controllers e views (MVC) ao projeto
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Permite servir arquivos estáticos (como CSS, JS e imagens) da pasta wwwroot
app.UseStaticFiles();

// Habilita o sistema de roteamento
app.UseRouting();

// Define a rota padrão para o aplicativo, direcionando para o controlador Football e a ação Index se nenhum outro for especificado
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Football}/{action=Index}/{id?}");

// Inicia o aplicativo
app.Run();
