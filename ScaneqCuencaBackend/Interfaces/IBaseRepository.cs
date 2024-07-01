using Microsoft.AspNetCore.Mvc;

namespace ScaneqCuencaBackend.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();

        TEntity? GetById(int id);
        TEntity? Add(TEntity entity);
        TEntity? Update(TEntity entity);
        TEntity? Delete(Guid id);
    }
}
