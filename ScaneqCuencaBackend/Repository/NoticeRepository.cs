using Microsoft.AspNetCore.Mvc;
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
            return _db.Notices.ToList();
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
                return entity;
            } catch (Exception)
            {
                return null;
            }
        }

        public IActionResult Update(Notice entity)
        {
            throw new NotImplementedException();
        }

        public IActionResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
