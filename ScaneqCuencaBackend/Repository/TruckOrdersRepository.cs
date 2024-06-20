using Microsoft.EntityFrameworkCore;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Interfaces;
using ScaneqCuencaBackend.Models.RequestModels;

namespace ScaneqCuencaBackend.Repository
{
    public class TruckOrdersRepository : IOrderRepository<TruckOrder>
    {
        private readonly SeqcuencabackendContext _db;

        public TruckOrdersRepository(SeqcuencabackendContext db)
        {
            _db = db;
        }

        public List<TruckOrder> GetAllWorkOrdersByCustomerId(int id)
        {
            return _db.TruckOrders.Where(x => x.CustomerId == id).ToList();
        }

        public List<TruckOrder> GetOrders()
        {
            return _db.TruckOrders
                .Include(bo => bo.Vehicle)
                .ThenInclude(bo => bo.Customer)
                .ToList();
        }

        public List<TruckOrder> GetOrdersByDateRange(WorkOrderDate dates)
        {
            return _db.TruckOrders
                .Where(entity => entity.DateField >= dates.startDate && entity.DateField <= dates.endDate)
                .Include(bo => bo.Vehicle)
                .ThenInclude(bo => bo.Customer)
                .ToList();
        }

        public List<TruckOrder> GetOrdersByFidRange(WorkOrderRange range)
        {
            return _db.TruckOrders
                .Where(entity => entity.Fid >= range.StartRangeNumber && entity.Fid <= range.EndRangeNumber)
                .Include(entity => entity.Customer)
                .ThenInclude(entity => entity.Vehicles)
                .ToList();
        }

        public TruckOrder? GetWorkOrderByNumber(int id)
        {
            return _db.TruckOrders
                .Where(x => x.Fid == id)
                .Include(bo => bo.Customer)
                .FirstOrDefault();
        }

        public List<TruckOrder> GetWorkOrderByVehicleId(int id)
        {
            return _db.TruckOrders
                .Where(x => x.VehicleId == id)
                .Include(bo => bo.Vehicle)
                .ToList();
        }

        public TruckOrder CreateWorkOrder(BusOrder model)
        {
            throw new NotImplementedException();
        }

        public TruckOrder? EditWorkOrder(WorkOrderEditRequestModel model)
        {
            var foundWorkOrder = _db.TruckOrders.FirstOrDefault(busr => busr.Fid == model.Fid);
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

        public TruckOrder? DeleteWorkOrder(int id)
        {
            var result = _db.TruckOrders.FirstOrDefault(entity => entity.Fid == id);

            if (result == null)
            {
                return null;
            }

            _db.TruckOrders.Remove(result);
            _db.SaveChanges();
            return result;
        }
    }
}
