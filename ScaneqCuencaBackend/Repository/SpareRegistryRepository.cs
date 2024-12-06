using Microsoft.EntityFrameworkCore;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Interfaces;

namespace ScaneqCuencaBackend.Repository
{
    public class SpareRegistryRepository : IBaseRepository<SpareRegister>
    {
        private readonly SeqcuencabackendContext _db;
        public SpareRegistryRepository(SeqcuencabackendContext db)
        {
            _db = db;
        }
        public List<SpareRegister> GetAll()
        {
            return _db.SpareRegisters.Include(x => x.SparePart).ToList();
        }

        public SpareRegister? GetById(int id)
        {
            return _db.SpareRegisters.Where(entity => entity.Id == id).FirstOrDefault();
        }

        public SpareRegister? Add(SpareRegister entity)
        {
            try
            {
                _db.SpareRegisters.Add(entity);
                _db.SaveChanges();
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SpareRegister> AddMultiple(List<SpareRegister> entities)
        {
            throw new NotImplementedException();
        }
        public SpareRegister? SumToQuantity(int id, int quantity)
        {
            var foundSpareRegister = _db.SpareRegisters.Where(_ => _.Id == id).FirstOrDefault();
            if (foundSpareRegister == null)
            {
                return null;
            }

            foundSpareRegister.Quantity += quantity;
            _db.SaveChanges();
            return foundSpareRegister;
        }
        public SpareRegister? SubstractToQuantity(int id, int quantity)
        {
            var foundSpareRegister = _db.SpareRegisters.Where(_ => _.Id == id).FirstOrDefault();
            if (foundSpareRegister == null)
            {
                return null;
            }
            var mathResult = foundSpareRegister.Quantity - quantity;
            if (mathResult <= 0)
            {
                _db.SpareRegisters.Remove(foundSpareRegister);
                _db.SaveChanges();
                return foundSpareRegister;
            } else
            {
                foundSpareRegister.Quantity = mathResult;
                _db.SaveChanges();
                return foundSpareRegister;
            }
        }

        public SpareRegister? Update(SpareRegister model)
        {
            try
            {
                var foundSpareRegister = _db.SpareRegisters.Where(entity => entity.Id == model.Id).FirstOrDefault();
                if (foundSpareRegister == null)
                {
                    return null;
                }

                _db.Entry(foundSpareRegister).CurrentValues.SetValues(model);
                _db.SaveChanges();

                return foundSpareRegister;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public SpareRegister? Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
