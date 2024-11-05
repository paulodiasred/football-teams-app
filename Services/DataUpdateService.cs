using System;
using System.Threading;
using System.Threading.Tasks;
using FootballTeamsApp.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class DataUpdateService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DataUpdateService> _logger;

    public DataUpdateService(IServiceProvider serviceProvider, ILogger<DataUpdateService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Define o intervalo de atualização (por exemplo, 6 horas)
        var intervalo = TimeSpan.FromMinutes(5);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    // Obtenha o contexto do banco de dados e o controlador com escopo
                    var context = scope.ServiceProvider.GetRequiredService<FootballDbContext>();
                    var controller = scope.ServiceProvider.GetRequiredService<FootballController>();

                    // Atualize os dados dos times
                    await controller.GetTeamsAsync();
                    
                    // Atualize a classificação do campeonato
                    var standings = await controller.GetStandingsAsync();
                    
                    // Opcional: Caso queira salvar a classificação no banco de dados, 
                    // posso adicionar lógica adicional para salvar os dados no contexto do banco (context).
                    // Exemplo:
                    // context.TeamStandings.UpdateRange(standings); // Se houver uma entidade 'TeamStandings' configurada
                    // await context.SaveChangesAsync();
                }

                _logger.LogInformation("Dados e classificação atualizados com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar dados e classificação da API.");
            }

            // Aguardando o próximo ciclo de execução
            await Task.Delay(intervalo, stoppingToken);
        }
    }
}
