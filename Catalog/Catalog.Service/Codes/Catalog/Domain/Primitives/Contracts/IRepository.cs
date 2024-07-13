using System.Linq.Expressions;

namespace eShop.Catalog.Domain.Primitives.Contracts
{
    public interface IRepository<TEntity, TId>
        where TEntity : IEntity<TId>
        where TId : notnull

    {
        //    void Add(TEntity entity); 
        //    void Delete(TEntity entity);
        //    void Update(TEntity entity);
        //    TEntity GetById(TId id); 

        //    IEnumerable<TEntity> GetEntities(); 



        Task AddAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(TId id);
        //Task< IEnumerable<TEntity>> GetEntitiesAysnc();
        Task<IEnumerable<TEntity>> FindEntitiesAysnc(Expression<Func<TEntity, bool>> predicate);
        //API
        Task<(IEnumerable<TEntity> Entities, int TotalRecordCount)> FilterAsync(string? criteria, string? sort, int pageSize, int pageIndex);
        Task<(IEnumerable<TEntity> Entities, int TotalRecordCount)> SearchAsync(string? text, string? sort, int pageSize, int pageIndex);











    }
}
