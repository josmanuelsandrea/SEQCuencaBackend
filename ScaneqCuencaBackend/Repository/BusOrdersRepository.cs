using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;

namespace ScaneqCuencaBackend.Repository
{
    public class BusOrdersRepository
    {
        private readonly SeqcuencabackendContext _db;
        public BusOrdersRepository(SeqcuencabackendContext db)
        {
            _db = db;
        }
        public BusOrder? getWorkOrderByNumber(int id)
        {
            return _db.BusOrders.Where(x => x.Fid == id).Include(bo => bo.Customer).FirstOrDefault();
        }

        public List<BusOrder> getAllWorkOrdersByCustomerId(int id)
        {
            return _db.BusOrders.Where(x => x.CustomerId == id).ToList();
        }

        public List<BusOrder> getWorkOrderByVehicleId(int id)
        {
            return _db.BusOrders.Where(x => x.VehicleId == id).ToList();
        }

        public BusOrder createWorkOrder(BusOrder model)
        {
            _db.BusOrders.Add(model);
            _db.SaveChanges();
            return model;
        }
    }
}
