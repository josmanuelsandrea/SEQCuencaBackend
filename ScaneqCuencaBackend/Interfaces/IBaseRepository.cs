using Microsoft.AspNetCore.Mvc;

namespace ScaneqCuencaBackend.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();

        TEntity? GetById(int id);
        TEntity? Add(TEntity entity);
        IActionResult Update(TEntity entity);
        IActionResult Delete(Guid id);
    }
}
