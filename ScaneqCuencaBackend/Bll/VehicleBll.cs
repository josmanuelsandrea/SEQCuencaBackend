using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class VehicleBll
    {
        private readonly SeqcuencabackendContext _db;
        private readonly BusOrdersRepository _BusOrderR;
        private readonly TruckOrdersRepository _TruckOrdersR;

        public VehicleBll(SeqcuencabackendContext db)
        {
            _db = db;
            _BusOrderR = new BusOrdersRepository(_db);
            _TruckOrdersR = new TruckOrdersRepository(_db);
        }

        public List<WorkOrderResponseModel> getWorkOrderByVehicleId(int id, string type)
        {
            var response = new List<WorkOrderResponseModel>();

            if (type == "Bus")
            {
                List<BusOrder> result = _BusOrderR.getWorkOrderByVehicleId(id);
                foreach (var workOrder in result)
                {
                    response.Add(new WorkOrderResponseModel()
                    {
                        Uid = workOrder.Id,
                        Fid = workOrder.Fid,
                        DateField = workOrder.DateField,
                        Customer = workOrder.CustomerId,
                        Description = workOrder.Description,
                        Iswarranty = workOrder.Iswarranty,
                        Kilometers = workOrder.Kilometers,
                        Isarchived = workOrder.Isarchived,
                        Storedvolume = workOrder.Storedvolume
                    });
                }

                return response;
            }
            else
            {
                List<TruckOrder> result = _TruckOrdersR.getWorkOrderByVehicleId(id);
                foreach (var workOrder in result)
                {
                    response.Add(new WorkOrderResponseModel()
                    {
                        Uid = workOrder.Id,
                        Fid = workOrder.Fid,
                        DateField = workOrder.DateField,
                        Customer = workOrder.CustomerId,
                        Description = workOrder.Description,
                        Iswarranty = workOrder.Iswarranty,
                        Kilometers = workOrder.Kilometers,
                        Isarchived = workOrder.Isarchived,
                        Storedvolume = workOrder.Storedvolume
                    });
                }

                return response;
            }
        }
    }
}
