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

        public List<BusOrder> GetOrders()
        {
            return _db.BusOrders
                .Include(bo => bo.Vehicle)
                .ThenInclude(bo => bo.Customer)
                .ToList();
        }

        public BusOrder? getWorkOrderByNumber(int id)
        {
            return _db.BusOrders
                .Where(x => x.Fid == id)
                .Include(bo => bo.Customer)
                .FirstOrDefault();
        }

        public List<BusOrder> getAllWorkOrdersByCustomerId(int id)
        {
            return _db.BusOrders.Where(x => x.CustomerId == id).ToList();
        }

        public List<BusOrder> getWorkOrderByVehicleId(int id)
        {
            return _db.BusOrders.Where(x => x.VehicleId == id).Include(bo => bo.Vehicle).ToList();
        }

        public BusOrder createWorkOrder(BusOrder model)
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
    }
}
