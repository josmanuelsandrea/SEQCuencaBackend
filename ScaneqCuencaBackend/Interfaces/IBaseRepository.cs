using Microsoft.AspNetCore.Mvc;

namespace ScaneqCuencaBackend.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity? GetById(int id);
        TEntity? Add(TEntity entity);
        List<TEntity> AddMultiple(List<TEntity> entities);
        TEntity? Update(TEntity entity);
        TEntity? Delete(Guid id);
    }
}
