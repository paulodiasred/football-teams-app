using Microsoft.EntityFrameworkCore;
using FootballTeamsApp.Models;

namespace FootballTeamsApp.Context
{
    // A classe FootballDbContext herda de DbContext e representa o contexto do banco de dados.
    public class FootballDbContext : DbContext
    {
        // Construtor que recebe as opções de configuração do contexto
        public FootballDbContext(DbContextOptions<FootballDbContext> options) : base(options) { }

        // DbSet representa as tabelas no banco de dados
        public DbSet<TeamStanding> TeamStandings { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<MatchStatistic> MatchStatistics { get; set; }
        public DbSet<Goalscorer> Goalscorers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Venue> Venues { get; set; }

        // Método que define as configurações de mapeamento para o banco de dados
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da entidade TeamStanding
            modelBuilder.Entity<TeamStanding>(entity =>
            {
                entity.HasKey(e => e.Id); // Define a chave primária como Id

                // Mapeamento de propriedades para colunas específicas no banco de dados
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.TeamId).HasColumnName("TeamId");
                entity.Property(e => e.TeamName).HasColumnName("TeamName");
                entity.Property(e => e.TeamBadge).HasColumnName("TeamBadge");
                entity.Property(e => e.OverallLeaguePosition).HasColumnName("Position");
                entity.Property(e => e.OverallLeaguePlayed).HasColumnName("Played");
                entity.Property(e => e.OverallLeagueWins).HasColumnName("Wins");
                entity.Property(e => e.OverallLeagueDraws).HasColumnName("Draws");
                entity.Property(e => e.OverallLeagueLosses).HasColumnName("Losses");
                entity.Property(e => e.OverallLeagueGoalsFor).HasColumnName("GoalsFor");
                entity.Property(e => e.OverallLeagueGoalsAgainst).HasColumnName("GoalsAgainst");
                entity.Property(e => e.OverallLeaguePoints).HasColumnName("Points");
                entity.Property(e => e.OverallPromotion).HasColumnName("Promotion");
            });

            // Configuração para garantir que TeamId seja único na tabela Teams
            modelBuilder.Entity<Team>()
                .HasIndex(t => t.TeamId)
                .IsUnique();

            // Configuração da entidade Team
            modelBuilder.Entity<Team>().HasKey(t => t.Id); // Define a chave primária
            modelBuilder.Entity<Team>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd(); // Id gerado automaticamente

            // Define TeamId como uma propriedade obrigatória
            modelBuilder.Entity<Team>().Property(t => t.TeamId).IsRequired();

            // Configuração do relacionamento 1-1 entre Team e Coach
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Coach)
                .WithOne(c => c.Team)
                .HasForeignKey<Coach>(c => c.TeamId) // Chave estrangeira em Coach referenciando Team
                .OnDelete(DeleteBehavior.Cascade); // Deleção em cascata

            // Configuração do relacionamento 1-N entre Team e Players
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Cascade); // Deleção em cascata

            // Configuração do relacionamento 1-1 entre Team e Venue
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Venue)
                .WithOne()
                .HasForeignKey<Team>(t => t.VenueId)
                .OnDelete(DeleteBehavior.SetNull); // Define VenueId como null se o Venue for deletado

            // Configuração da entidade Coach
            modelBuilder.Entity<Coach>().HasKey(c => c.Id); // Define a chave primária
            modelBuilder.Entity<Coach>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd(); // Id gerado automaticamente

            // Configuração da entidade Player
            modelBuilder.Entity<Player>().ToTable("Player"); // Nome da tabela "Player"
            modelBuilder.Entity<Player>().HasKey(p => p.PlayerId); // Define a chave primária
            modelBuilder.Entity<Player>()
                .Property(p => p.PlayerId)
                .ValueGeneratedOnAdd(); // PlayerId gerado automaticamente

            // Configuração da entidade Venue
            modelBuilder.Entity<Venue>().HasKey(v => v.VenueId); // Define a chave primária
            modelBuilder.Entity<Venue>()
                .Property(v => v.VenueId)
                .ValueGeneratedOnAdd(); // VenueId gerado automaticamente

            // Configuração da entidade Match
            modelBuilder.Entity<Match>().HasKey(m => m.Id); // Define a chave primária
            modelBuilder.Entity<Match>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd(); // Id gerado automaticamente

            // Define a relação entre Match e Team (chave estrangeira TeamId)
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Team)
                .WithMany()
                .HasForeignKey(m => m.TeamId)
                .IsRequired(); // TeamId é obrigatório

            // Configura RowVersion para controle de versão e concorrência
            modelBuilder.Entity<Match>()
                .Property(m => m.RowVersion)
                .IsRowVersion(); // Define RowVersion para detecção de conflitos de atualização

            // Configuração da entidade MatchStatistic e seu relacionamento com Match
            modelBuilder.Entity<MatchStatistic>().HasKey(ms => ms.Id); // Define a chave primária
            modelBuilder.Entity<MatchStatistic>()
                .Property(ms => ms.Id)
                .ValueGeneratedOnAdd(); // Id gerado automaticamente

            // Relacionamento 1-N entre Match e MatchStatistics
            modelBuilder.Entity<MatchStatistic>()
                .HasOne(ms => ms.Match)
                .WithMany(m => m.MatchStatistics)
                .HasForeignKey(ms => ms.MatchId)
                .OnDelete(DeleteBehavior.Cascade); // Deleção em cascata

            // Configuração da entidade Goalscorer e seu relacionamento com Match
            modelBuilder.Entity<Goalscorer>().HasKey(g => g.Id); // Define a chave primária
            modelBuilder.Entity<Goalscorer>()
                .Property(g => g.Id)
                .ValueGeneratedOnAdd(); // Id gerado automaticamente

            // Relacionamento entre Goalscorer e Match
            modelBuilder.Entity<Goalscorer>()
                .HasOne(g => g.Match)
                .WithMany(m => m.Goalscorers)
                .HasForeignKey(g => g.MatchId);

            // Configuração da entidade Card e seu relacionamento com Match
            modelBuilder.Entity<Card>().HasKey(c => c.Id); // Define a chave primária
            modelBuilder.Entity<Card>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd(); // Id gerado automaticamente

            // Relacionamento entre Card e Match
            modelBuilder.Entity<Card>()
                .HasOne(c => c.Match)
                .WithMany(m => m.Cards)
                .HasForeignKey(c => c.MatchId);
        }
    }
}
