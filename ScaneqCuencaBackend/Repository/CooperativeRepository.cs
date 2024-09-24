using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Interfaces;

namespace ScaneqCuencaBackend.Repository
{
    public class CooperativeRepository : IBaseRepository<Cooperative>
    {
        private readonly SeqcuencabackendContext _db;
        public CooperativeRepository(SeqcuencabackendContext context)
        {
            _db = context;
        }
        public Cooperative? Add(Cooperative entity)
        {
            try
            {
                _db.Cooperatives.Add(entity);
                return entity;
            } catch (Exception)
            {
                return null;
            }
        }

        public List<Cooperative> GetAll()
        {
            return _db.Cooperatives.ToList();
        }

        public Cooperative? GetById(int id)
        {
            return _db.Cooperatives.Where(x => x.Id == id).FirstOrDefault();
        }

        public Cooperative? Update(Cooperative model)
        {
            try
            {
                var foundCooperative = _db.Cooperatives.Where(entity => entity.Id == model.Id).FirstOrDefault();
                if (foundCooperative == null)
                {
                    return null;
                }

                _db.Entry(foundCooperative).CurrentValues.SetValues(model);
                _db.SaveChanges();

                return foundCooperative;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Cooperative? Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Cooperative> AddMultiple(List<Cooperative> entities)
        {
            throw new NotImplementedException();
        }
    }
}
