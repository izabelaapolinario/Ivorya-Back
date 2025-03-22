using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ivorya_back.Data;
using System.IO;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Carrega a configuração do appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())  // Define o diretório base para a leitura
            .AddJsonFile("appsettings.json")               // Adiciona o arquivo de configuração
            .Build();

        // Obtenha a string de conexão do appsettings.json
        var connectionString = configuration.GetConnectionString("POSTGRES");

        optionsBuilder.UseNpgsql(connectionString, options =>
            options.MigrationsHistoryTable("__EFMigrationsHistory", configuration["DatabaseSettings:Schema"]));  // Usa o schema de migrações

        // Crie e retorne o contexto
        return new ApplicationDbContext(optionsBuilder.Options, configuration);
    }
}
