﻿using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class NoticeBll
    {
        private readonly SeqcuencabackendContext _db;
        private readonly NoticeRepository _repository;
        public NoticeBll(SeqcuencabackendContext db, NoticeRepository repository)
        {
            _db = db;
            _repository = repository;
        }

        public List<Notice> GetAll()
        {
            return _repository.GetAll();
        }

        public Notice? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<Notice> GetNoticesByVehicleId(int vehicleId)
        {
            var notices = _repository.GetAll()
                .Where(entity => entity.VehicleId == vehicleId)
                .ToList();
            return notices;
        }

        public async Task<List<Notice>> GetNoticesByVehicleIdAsync(int vehicleId)
        {
            var notices = await _repository.GetAllAsync();
            var result = notices.Where(entity => entity.VehicleId == vehicleId).ToList();
            return result;
        }

        public Notice? Add(Notice entity)
        {
            entity.Resolved = false;
            return _repository.Add(entity);
        }

        public Notice? Update(Notice entity)
        {
            return _repository.Update(entity);
        }
    }
}
