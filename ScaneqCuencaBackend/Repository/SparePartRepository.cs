using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Interfaces;

namespace ScaneqCuencaBackend.Repository
{
    public class SparePartRepository : IBaseRepository<SparePart>
    {
        private readonly SeqcuencabackendContext _db;
        public SparePartRepository(SeqcuencabackendContext db)
        {
            _db = db;
        }
        public List<SparePart> GetAll()
        {
            return _db.SpareParts.ToList();
        }

        public SparePart? GetById(int id)
        {
            return _db.SpareParts.Where(entity => entity.Id == id).FirstOrDefault();
        }

        public SparePart? GetByCode(string code)
        {
            return _db.SpareParts.Where(entity => entity.Code == code).FirstOrDefault();
        }

        public SparePart? Add(SparePart entity)
        {
            try
            {
                _db.SpareParts.Add(entity);
                _db.SaveChanges();
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SparePart> AddMultiple(List<SparePart> entities)
        {
            throw new NotImplementedException();
        }

        public SparePart? Update(SparePart model)
        {
            try
            {
                var foundSparePart = _db.SpareParts.Where(entity => entity.Id == model.Id).FirstOrDefault();
                if (foundSparePart == null)
                {
                    return null;
                }

                _db.Entry(foundSparePart).CurrentValues.SetValues(model);
                _db.SaveChanges();

                return foundSparePart;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public SparePart? Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
