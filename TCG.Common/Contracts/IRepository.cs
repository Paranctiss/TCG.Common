using System.Linq.Expressions;

namespace TCG.Common.Contracts;

/// <summary>
/// Interface générique pour les opérations CRUD de base sur une entité donnée.
/// </summary>
/// <typeparam name="T">Type de l'entité</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Récupère une entité à partir de son identifiant unique.
    /// </summary>
    /// <param name="id">Identifiant unique de l'entité à récupérer.</param>
    /// <param name="cancellationToken">Token d'annulation pour annuler l'opération asynchrone.</param>
    /// <returns>Task de l'entité récupérée ou null si elle n'existe pas.</returns>
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Récupère une entité à partir de son identifiant unique.
    /// </summary>
    /// <param name="id">Identifiant unique de l'entité à récupérer.</param>
    /// <param name="cancellationToken">Token d'annulation pour annuler l'opération asynchrone.</param>
    /// <returns>Task de l'entité récupérée ou null si elle n'existe pas.</returns>
    Task<T> GetByGUIDAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Récupère toutes les entités du type T.
    /// </summary>
    /// <param name="cancellationToken">Token d'annulation pour annuler l'opération asynchrone.</param>
    /// <returns>Task d'une liste d'entités récupérées ou une liste vide si aucune entité n'est trouvée.</returns>
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Ajoute une nouvelle entité dans la source de données.
    /// </summary>
    /// <param name="entity">Entité à ajouter.</param>
    /// <param name="cancellationToken">Token d'annulation pour annuler l'opération asynchrone.</param>
    /// <returns>Task représentant l'opération asynchrone.</returns>
    Task AddAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Met à jour une entité existante dans la source de données.
    /// </summary>
    /// <param name="entity">Entité à mettre à jour.</param>
    /// <param name="cancellationToken">Token d'annulation pour annuler l'opération asynchrone.</param>
    /// <returns>Task représentant l'opération asynchrone.</returns>
    Task UpdateAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Supprime une entité existante de la source de données à partir de son identifiant unique.
    /// </summary>
    /// <param name="id">Identifiant unique de l'entité à supprimer.</param>
    /// <param name="cancellationToken">Token d'annulation pour annuler l'opération asynchrone.</param>
    /// <returns>Task représentant l'opération asynchrone.</returns>
    Task RemoveAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Supprime une entité existante de la source de données à partir de son identifiant unique.
    /// </summary>
    /// <param name="id">Identifiant unique de l'entité à supprimer.</param>
    /// <param name="cancellationToken">Token d'annulation pour annuler l'opération asynchrone.</param>
    /// <returns>Task représentant l'opération asynchrone.</returns>
    Task RemoveByGUIDAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Execute toute les fonctions au sein de la même transaction
    /// </summary>
    /// <param name="action">The action to execute within the transaction.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves a paginated list of public sale posts with optional sorting, filtering, and paging.
    /// </summary>
    /// <typeparam name="TOrderKey">Le type pour pouvoir order</typeparam>
    /// <param name="pageNumber">Nombre de page</param>
    /// <param name="pageSize">Nombre item par page</param>
    /// <param name="cancellationToken">TCancellation token</param>
    /// <param name="orderBy">Specifier le tri : orderBy (optional)</param>
    /// <param name="descending">Le type pour order Desc or Ascend (default: true)</param>
    /// <param name="filter">Les filtres ajouter (optional)</param>
    /// <returns>A task representing the asynchronous operation. The result is an enumerable collection of type T.</returns>
    Task<IEnumerable<T>> GetAllSalePostPublicAsync<TOrderKey>(
        int pageNumber, int pageSize,
        CancellationToken cancellationToken,
        Expression<Func<T, TOrderKey>> orderBy = null,
        bool descending = true,
        Expression<Func<T, bool>> filter = null);
}