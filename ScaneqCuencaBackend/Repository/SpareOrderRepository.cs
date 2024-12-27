using Microsoft.EntityFrameworkCore;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Interfaces;

namespace ScaneqCuencaBackend.Repository
{
    public class SpareOrderRepository : IBaseRepository<SpareOrder>
    {
        private readonly SeqcuencabackendContext _db;
        public SpareOrderRepository(SeqcuencabackendContext db)
        {
            _db = db;
        }
        public List<SpareOrder> GetAll()
        {
            return _db.SpareOrders
                .Include(x => x.BusOrder)
                    .ThenInclude(x => x.Customer)
                    .ThenInclude(x => x.Vehicles)
                    .ThenInclude(x => x.Cooperative)
                .Include(x => x.Customer)
                .Include(entity => entity.SpareRegisters)
                    .ThenInclude(register => register.SparePart)
                .ToList();
        }

        public SpareOrder? GetById(int id)
        {
            return _db.SpareOrders
                .Where(entity => entity.Id == id)
                .Include(x => x.BusOrder)
                    .ThenInclude(x => x.Customer)
                .Include(x => x.Customer)
                .Include(entity => entity.SpareRegisters)
                    .ThenInclude(register => register.SparePart)
                .FirstOrDefault();
        }

        public SpareOrder? Add(SpareOrder entity)
        {
            try
            {
                _db.SpareOrders.Add(entity);
                _db.SaveChanges();
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SpareOrder> AddMultiple(List<SpareOrder> entities)
        {
            throw new NotImplementedException();
        }

        public SpareOrder? Update(SpareOrder model)
        {
            try
            {
                var foundSpareOrder = _db.SpareOrders.Where(entity => entity.Id == model.Id).FirstOrDefault();
                if (foundSpareOrder == null)
                {
                    return null;
                }

                _db.Entry(foundSpareOrder).CurrentValues.SetValues(model);
                _db.SaveChanges();

                return foundSpareOrder;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public SpareOrder? Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
