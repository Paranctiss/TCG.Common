using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TCG.Common.Contracts;
using TCG.Common.Settings;

namespace TCG.Common.MySqlDb;

public static class DependencyInjectionMySql
{
    public static IServiceCollection AddPersistence<TDbContext>(this IServiceCollection services, IConfiguration configuration)
        where TDbContext : DbContext
    {
        _ = services.AddDbContext<TDbContext>(
            options =>
            {
                var mysqlSettings = configuration.GetSection("MySqlDatabaseSettings").Get<SqlDbSettings>();
                options.UseMySQL(mysqlSettings.ConnectionString);
            });

        services.Scan(scan => scan
            // Moins précis mais scan toutes les dépendances de l'assembly qui s'exécute
            .FromApplicationDependencies()
            // Plus précis si on est certains que toutes les implémentations se trouvent dans l'assembly du TDbContext
            //.FromAssembliesOf(typeof(TDbContext))
            .AddClasses(c => c.AssignableTo(typeof(IRepository<>)))
            // A utiliser si le repo n'est pas nommé à partir de l'interface (sans le "I").
            // /!\ AsImplementedInterfaces enregistrera le type concret pour toutes les interfaces implémentées (y compris IRepository<EntityName>). Possibilité de filtrer les interfaces à enregistrer avec un prédicat.
            //.AsImplementedInterfaces()
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }
}
