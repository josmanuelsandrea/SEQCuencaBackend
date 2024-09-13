using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Interfaces;

namespace ScaneqCuencaBackend.Repository
{
    public class NoticeRepository : IBaseRepository<Notice>
    {
        private readonly SeqcuencabackendContext _db;
        public NoticeRepository(SeqcuencabackendContext db)
        {
            _db = db;
        }
        public List<Notice> GetAll()
        {
            return _db.Notices.Include(n => n.Vehicle).ThenInclude(v => v.Customer).ToList();
        }

        public Task<List<Notice>> GetAllAsync()
        {
            return _db.Notices.ToListAsync();
        }

        public Notice? GetById(int id)
        {
            return _db.Notices.Where(entity => entity.Id == id).FirstOrDefault();
        }

        public Notice? Add(Notice entity)
        {
            try
            {
                _db.Notices.Add(entity);
                _db.SaveChanges();
                return entity;
            } catch (Exception)
            {
                return null;
            }
        }

        public Notice? Update(Notice model)
        {
            try
            {
                var foundNotice = _db.Notices.Where(entity => entity.Id == model.Id).FirstOrDefault();
                if (foundNotice == null)
                {
                    return null;
                }

                var vehicleId = foundNotice.VehicleId;
                var date = foundNotice.NoticeDate;
                _db.Entry(foundNotice).CurrentValues.SetValues(model);
                foundNotice.VehicleId = vehicleId;
                foundNotice.NoticeDate = date;
                _db.SaveChanges();

                return foundNotice;
            } catch (Exception)
            {
                return null;
            }
        }

        public Notice? Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Notice> AddMultiple(List<Notice> entities)
        {
            throw new NotImplementedException();
        }
    }
}
