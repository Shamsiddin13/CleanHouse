using Microsoft.EntityFrameworkCore;
using CleanHouse.Data.IRepositories;
using CleanHouse.Data.DbContexts;
using CleanHouse.Domain.Commons;

namespace CleanHouse.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    protected readonly CleanHouseDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(CleanHouseDbContext dbContext)
    {
        _dbContext = dbContext; 
        _dbSet = _dbContext.Set<TEntity>();  
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        _dbSet.Remove(entity);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var entry = await _dbSet.AddAsync(entity);

        await _dbContext.SaveChangesAsync();
        
        return entry.Entity;

    }

    public IQueryable<TEntity> SelectAll() 
        => _dbSet;
    

    public async Task<TEntity> SelectByIdAsync(long id) 
        => await _dbSet.FirstOrDefaultAsync(e => e.Id == id);   

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entry = _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();    
        
        return entry.Entity;
    }
}
