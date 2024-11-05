using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FootballTeamsApp.Models;
using FootballTeamsApp.Context;
using Microsoft.EntityFrameworkCore;

public class FootballController : Controller
{
    // Declara o cliente HTTP para chamadas de API e o contexto do banco de dados
    private readonly HttpClient _httpClient;
    private readonly FootballDbContext _context;

    // Construtor que inicializa o HttpClient e o DbContext (injeção de dependências)
    public FootballController(HttpClient httpClient, FootballDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    // Método que exibe a classificação na página inicial
    public async Task<IActionResult> Index()
    {
        var standings = await GetStandingsAsync(); // Busca a classificação da API
        return View("Standings", standings); // Renderiza a View "Standings" com os dados
    }

    // Método que obtém a classificação atual do campeonato via API
    public async Task<List<TeamStanding>> GetStandingsAsync()
    {
        var standingsUrl = "https://apiv3.apifootball.com/?action=get_standings&league_id=99&APIkey=915e442f5fb7409205728caa317a309d0dc39d234ac093b40081562ccce6c5a9";

        try
        {
            var response = await _httpClient.GetAsync(standingsUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var standings = JsonConvert.DeserializeObject<List<TeamStanding>>(jsonString);

                if (standings != null)
                {
                    // Remove os dados antigos da tabela de classificações
                    _context.TeamStandings.RemoveRange(_context.TeamStandings);
                    await _context.SaveChangesAsync();

                    // Adiciona os novos dados da classificação
                    await _context.TeamStandings.AddRangeAsync(standings);
                    await _context.SaveChangesAsync();

                    return standings;
                }
            }
            else
            {
                Console.WriteLine($"Erro ao obter a classificação: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exceção ao chamar a API: {ex.Message}");
        }
        return new List<TeamStanding>(); // Retorna uma lista vazia se houver falha
    }

    // Método para atualização manual da classificação via endpoint
    [HttpGet("Football/UpdateStandings")]
    public async Task<IActionResult> UpdateStandings()
    {
        await GetStandingsAsync(); // Atualiza a classificação chamando o método anterior
        return Ok("Classificação atualizada com sucesso.");
    }

    // Método que busca e atualiza dados dos times a partir da API
    public async Task<List<Team>> GetTeamsAsync()
    {
        Console.WriteLine("Atualizando dados dos times...");
        var url = "https://apiv3.apifootball.com/?action=get_teams&APIkey=915e442f5fb7409205728caa317a309d0dc39d234ac093b40081562ccce6c5a9&league_id=99";
        var teams = new List<Team>();

        try
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                teams = JsonConvert.DeserializeObject<List<Team>>(jsonString) ?? new List<Team>();

                // Carrega os TeamIds existentes para evitar duplicação
                var existingTeamIds = await _context.Teams.Select(t => t.TeamId).ToListAsync();

                foreach (var team in teams)
                {
                    if (!existingTeamIds.Contains(team.TeamId))
                    {
                        // Associa o treinador ao time, se houver
                        if (team.coaches != null && team.coaches.Any())
                        {
                            team.Coach = team.coaches.First();
                            team.Coach.TeamId = team.TeamId;
                            _context.Coaches.Add(team.Coach);
                        }
                        _context.Teams.Add(team);
                    }
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine($"Erro ao chamar a API: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exceção ao chamar a API: {ex.Message}");
        }

        // Retorna a lista de times com seus treinadores
        return await _context.Teams.Include(t => t.Coach).ToListAsync();
    }

    // Exibe detalhes do time especificado
    [HttpGet("Football/TeamDetails/{teamId}")]
    public async Task<IActionResult> TeamDetails(int teamId)
    {
        if (teamId == 0) return BadRequest("Id do time inválido.");

        var team = await _context.Teams
            .Include(t => t.Coach)
            .Include(t => t.Venue)
            .Include(t => t.Players)
            .FirstOrDefaultAsync(t => t.TeamId == teamId);

        if (team == null) return NotFound("Time não encontrado.");

        return View(team); // Renderiza a View "TeamDetails" com o time
    }

    // Exibe as partidas de um time específico usando o teamKey
    [HttpGet("Football/Matches/{teamKey}")]
    public async Task<IActionResult> Matches(string teamKey)
    {
        if (string.IsNullOrEmpty(teamKey)) return BadRequest("O parâmetro teamKey é obrigatório.");

        var team = await _context.Teams
            .Include(t => t.Coach)
            .Include(t => t.Venue)
            .FirstOrDefaultAsync(t => t.team_key == teamKey);

        if (team == null) return NotFound("Time não encontrado.");

        var matches = await GetMatchesForTeamAsync(teamKey);
        if (matches.Any()) await SaveMatchesAndDetailsAsync(matches, team.Id);

        matches = await _context.Matches
            .Include(m => m.MatchStatistics)
            .Include(m => m.Cards)
            .Include(m => m.Goalscorers)
            .Where(m => m.TeamId == team.Id)
            .OrderByDescending(m => m.MatchDate)
            .ToListAsync();

        ViewBag.TeamName = team.team_name;
        ViewBag.TeamBadge = team.team_badge;
        return View("Matches", matches);
    }

    // Método para buscar jogadores de um time especifíco para view
    [HttpGet("Football/TeamPlayers/{teamId}")]
    public async Task<IActionResult> TeamPlayers(int teamId)
    {
        var team = await _context.Teams
            .Include(t => t.Players) // Inclui os jogadores associados ao time
            .FirstOrDefaultAsync(t => t.TeamId == teamId);

        if (team == null)
        {
            return NotFound("Time não encontrado.");
        }

        return View(team); // Passa o time, incluindo seus jogadores, para a view
    }


    // Método para buscar partidas de um time específico via API
    private async Task<List<Match>> GetMatchesForTeamAsync(string teamKey)
    {
        Console.WriteLine($"Chamando a API para buscar partidas do time com chave {teamKey}");

        var startYear = DateTime.Now.Year;
        var fromDate = new DateTime(startYear, 4, 1).ToString("yyyy-MM-dd");
        var toDate = DateTime.Now.ToString("yyyy-MM-dd");
        var leagueId = "99";

        var url = $"https://apiv3.apifootball.com/?action=get_events&from={fromDate}&to={toDate}&league_id={leagueId}&team_id={teamKey}&APIkey=915e442f5fb7409205728caa317a309d0dc39d234ac093b40081562ccce6c5a9";

        try
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Match>>(jsonString) ?? new List<Match>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exceção ao chamar a API: {ex.Message}");
        }

        return new List<Match>();
    }

    // Salva ou atualiza as partidas no banco de dados
    private async Task SaveMatchesAndDetailsAsync(List<Match> matches, int teamId)
    {
        _context.ChangeTracker.AutoDetectChangesEnabled = false;

        foreach (var match in matches)
        {
            match.TeamId = teamId;

            var existingMatch = await _context.Matches
                .Include(m => m.MatchStatistics)
                .Include(m => m.Cards)
                .Include(m => m.Goalscorers)
                .FirstOrDefaultAsync(m => m.TeamId == teamId &&
                                          m.MatchDate == match.MatchDate &&
                                          m.HomeTeam == match.HomeTeam &&
                                          m.AwayTeam == match.AwayTeam);

            if (existingMatch == null)
            {
                _context.Matches.Add(match);
            }
            else
            {
                existingMatch.HomeScore = match.HomeScore;
                existingMatch.AwayScore = match.AwayScore;
                existingMatch.MatchStadium = match.MatchStadium;
                existingMatch.MatchReferee = match.MatchReferee;

                // Remove e substitui estatísticas, cartões e artilheiros antigos
                _context.MatchStatistics.RemoveRange(existingMatch.MatchStatistics);
                _context.Cards.RemoveRange(existingMatch.Cards);
                _context.Goalscorers.RemoveRange(existingMatch.Goalscorers);

                existingMatch.MatchStatistics = match.MatchStatistics ?? new List<MatchStatistic>();
                existingMatch.Cards = match.Cards ?? new List<Card>();
                existingMatch.Goalscorers = match.Goalscorers ?? new List<Goalscorer>();
            }
        }

        await _context.SaveChangesAsync();
        _context.ChangeTracker.AutoDetectChangesEnabled = true;
    }

    // Endpoint para limpar partidas duplicadas no banco de dados
    [HttpGet("Football/CleanDuplicates")]
    public async Task<IActionResult> CleanDuplicates()
    {
        await RemoveDuplicateMatchesAsync();
        return Ok("Duplicatas removidas.");
    }

    // Método que identifica e remove partidas duplicadas
    public async Task RemoveDuplicateMatchesAsync()
    {
        var duplicates = await _context.Matches
            .GroupBy(m => new { m.TeamId, m.MatchDate, m.HomeTeam, m.AwayTeam })
            .Where(g => g.Count() > 1)
            .SelectMany(g => g.OrderByDescending(m => m.Id).Skip(1))
            .ToListAsync();

        if (duplicates.Any())
        {
            _context.Matches.RemoveRange(duplicates);
            await _context.SaveChangesAsync();
        }

        Console.WriteLine($"{duplicates.Count} duplicatas removidas.");
    }
}
