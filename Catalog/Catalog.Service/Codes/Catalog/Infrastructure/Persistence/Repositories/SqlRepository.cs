using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using eShop.Catalog.Domain.Primitives;
using eShop.Catalog.Domain.Primitives.Contracts;
using Microsoft.EntityFrameworkCore;

namespace eShop.Catalog.Infrastructure.Persistence.Repositories;

public abstract class SqlRepository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : notnull
{
    private readonly DbContext _context;
    // private DbSet<TEntity> set;
    protected DbSet<TEntity> set;

    public SqlRepository(DbContext context)
    {
        _context = context;
        set = context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        await set.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        set.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await set.FindAsync(id);
    }

    //public async Task<IEnumerable<TEntity>> FindEntitiesAsync(
    //    //? Compile
    //    Expression<Func<TEntity, bool>> predicate
    //)
    //{
    //    return await set.Where(predicate).ToListAsync();
    //}

    public async Task UpdateAsync(TEntity entity)
    {
        var entry = _context.Entry(entity);
        entry.State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    // public async Task<IEnumerable<TEntity>> FilterAsync(
    //     string? criteria,
    //     string? sort,
    //     int pageSize,
    //     int pageIndex
    // )
    // {
    //     //? criteria --> DyanmicLinq --> Parse --> Expression
    //     //? Expression --> EF --> SQL
    //     //? ORM --> Change Tracker
    //     // var query = set.AsQueryable();
    //     var query = set.AsNoTracking();

    //     if (!string.IsNullOrEmpty(criteria))
    //     {
    //         query = query.Where(criteria);
    //     }

    //     if (!string.IsNullOrEmpty(sort))
    //     {
    //         query = query.OrderBy(sort);
    //     }

    //     //Paging(Skip, Take)

    //     var entities = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
    //     return entities;
    //     // return await query.ToListAsync();
    // }

    public async Task<(IEnumerable<TEntity> Entities, int TotalRecordCount)> FilterAsync(
        string? criteria,
        string? sort,
        int pageSize,
        int pageIndex
    )
    {
        var query = set.AsNoTracking();

        if (!string.IsNullOrEmpty(criteria))
        {
            query = query.Where(criteria);
        }

        if (!string.IsNullOrEmpty(sort))
        {
            query = query.OrderBy(sort);
        }

        //? select count(*) from products where ...
        var entities = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        var totalRecordCount = await query.CountAsync();

        return (entities, totalRecordCount);
    }

    public virtual Task<(IEnumerable<TEntity> Entities, int TotalRecordCount)> SearchAsync(
        string? text,
        string? sort,
        int pageSize,
        int pageIndex
    )
    {
        //? Reflection
        //? DRY
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TEntity>> FindEntitiesAysnc(Expression<Func<TEntity, bool>> predicate)
    {
        return await set.Where(predicate).ToListAsync();
    }
}
