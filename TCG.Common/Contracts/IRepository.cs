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
}