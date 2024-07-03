using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Interfaces;
using ScaneqCuencaBackend.Models.RequestModels;

namespace ScaneqCuencaBackend.Repository
{
    public class BusOrdersRepository : IOrderRepository<BusOrder>
    {
        private readonly SeqcuencabackendContext _db;
        public BusOrdersRepository(SeqcuencabackendContext db)
        {
            _db = db;
        }

        public List<BusOrder> GetOrders()
        {
            return _db.BusOrders
                .Include(bo => bo.Vehicle)
                .ThenInclude(bo => bo.Customer)
                .ToList();
        }

        public async Task<List<BusOrder>> GetOrdersAsync()
        {
            return await _db.BusOrders
                .Include(bo => bo.Vehicle)
                .ThenInclude(bo => bo.Customer)
                .OrderBy(entity => entity.Fid)
                .ToListAsync();
        }

        public List<BusOrder> GetOrdersByDateRange(WorkOrderDate dates)
        {
            return _db.BusOrders
                .Where(entity => entity.DateField >= dates.startDate && entity.DateField <= dates.endDate)
                .Include(bo => bo.Vehicle)
                .ThenInclude(bo => bo.Customer)
                .ToList();
        }

        public List<BusOrder> GetOrdersByFidRange(WorkOrderRange range)
        {
            return _db.BusOrders
                .Where(entity => entity.Fid >= range.StartRangeNumber && entity.Fid <= range.EndRangeNumber)
                .Include(entity => entity.Customer)
                .ThenInclude(entity => entity.Vehicles)
                .ToList();
        }

        public BusOrder? GetWorkOrderByNumber(int id)
        {
            return _db.BusOrders
                .Where(x => x.Fid == id)
                .Include(bo => bo.Customer)
                .FirstOrDefault();
        }

        public List<BusOrder> GetAllWorkOrdersByCustomerId(int id)
        {
            return _db.BusOrders.Where(x => x.CustomerId == id).ToList();
        }

        public List<BusOrder> GetWorkOrderByVehicleId(int id)
        {
            return _db.BusOrders
                .Where(x => x.VehicleId == id)
                .Include(bo => bo.Vehicle)
                .ThenInclude(bo => bo.Customer)
                .ToList();
        }

        public Task<List<BusOrder>> GetWorkOrderByVehicleIdAsync(int id)
        {
            return _db.BusOrders
                .Where(x => x.VehicleId == id)
                .Include(bo => bo.Vehicle)
                .ThenInclude(bo => bo.Customer)
                .ToListAsync();
        }

        public BusOrder CreateWorkOrder(BusOrder model)
        {
            _db.BusOrders.Add(model);
            _db.SaveChanges();
            return model;
        }

        public BusOrder? EditWorkOrder(WorkOrderEditRequestModel model)
        {
            var foundWorkOrder = _db.BusOrders.FirstOrDefault(busr => busr.Fid == model.Fid);
            if (foundWorkOrder == null)
            {
                return null;
            }

            // Guardar el CustomerId actual para preservarlo
            var customerId = foundWorkOrder.CustomerId;

            // Actualizar las propiedades de foundWorkOrder con los valores del modelo proporcionado
            _db.Entry(foundWorkOrder).CurrentValues.SetValues(model);

            // Restaurar el CustomerId después de la actualización
            foundWorkOrder.CustomerId = customerId;

            // Guardar los cambios en la base de datos
            _db.SaveChanges();

            // Retornar la orden de trabajo actualizada
            return foundWorkOrder;
        }

        public BusOrder? DeleteWorkOrder(int id)
        {
            var result = _db.BusOrders.FirstOrDefault(entity => entity.Fid == id);

            if (result == null)
            {
                return null;
            }

            _db.BusOrders.Remove(result);
            _db.SaveChanges();
            return result;
        }
    }
}
