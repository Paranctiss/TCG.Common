using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TCG.Common.Contracts;

namespace TCG.Common.MySqlDb;
/// <summary>
/// Generic implementation of IRepository interface.
/// </summary>
public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{T}"/> class with the specified database context.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().FindAsync(id,cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<T> GetByGUIDAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().FindAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task RemoveAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    /// <inheritdoc/>
    public async Task RemoveByGUIDAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByGUIDAsync(id, cancellationToken);
        if (entity != null)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    
    public async Task<IEnumerable<T>> GetAllSalePostPublicAsync<TOrderKey>(
        int pageNumber, int pageSize,
        CancellationToken cancellationToken,
        Expression<Func<T, TOrderKey>> orderBy = null, 
        bool descending = true, 
        Expression<Func<T,bool>> filter = null)
    {
        var query = _dbContext.Set<T>().AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }
        
        if (orderBy != null)
        {
            if (descending)
            {
                query = query.OrderByDescending(orderBy);
            }
            else
            {
                query = query.OrderBy(orderBy);
            }
        }
        return await query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    }
    
    public async Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken)
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                await action();

                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
