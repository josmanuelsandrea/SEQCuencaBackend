using AutoMapper;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class VehicleBll
    {
        private readonly SeqcuencabackendContext _db;
        private readonly BusOrdersRepository _BusOrderR;
        private readonly TruckOrdersRepository _TruckOrdersR;
        private readonly VehicleRepository _VehicleRepository;

        public VehicleBll(SeqcuencabackendContext db, IMapper mapper)
        {
            _db = db;
            _BusOrderR = new BusOrdersRepository(_db);
            _TruckOrdersR = new TruckOrdersRepository(_db);
            _VehicleRepository = new VehicleRepository(_db, mapper);
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
        public Vehicle createVehicle(VehicleCreateRequest model, IMapper mapper)
        {
            return _VehicleRepository.createVehicle(model);
        }
    }
}
