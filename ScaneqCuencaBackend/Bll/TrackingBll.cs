using AutoMapper;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class TrackingBll
    {
        private readonly SeqcuencabackendContext _db;
        private readonly VehicleRepository _vehicleRepository;
        private readonly MaintenanceRegistryBll _maintenanceRegistryB;
        private readonly BusOrderBll _busOrderB;
        private readonly IMapper _mapper;
        public TrackingBll(SeqcuencabackendContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _vehicleRepository = new(db, mapper);
            _maintenanceRegistryB = new(db);
            _busOrderB = new(db, mapper);
        }

        public List<TrackingDataResponse> GetTrackingData()
        {
            var vehicleList = _vehicleRepository.GetAllVehicles();
            List<TrackingDataResponse> response = new ();

            // Find all registries by vehicle
            foreach (var vehicle in vehicleList)
            {
                if (vehicle.Model == "generic")
                {
                    continue;
                }
                // General data
                var allRegistries = _maintenanceRegistryB.GetByVehicleId(vehicle.Id);
                var lastOilEngineWorkOrder = allRegistries.Where(registry => registry.MaintenanceType == "oil_engine_15" || registry.MaintenanceType == "oil_engine_10").FirstOrDefault();

                if (lastOilEngineWorkOrder == null)
                {
                    continue;
                }

                var workOrders = _busOrderB.GetWorkOrderByVehicleId(vehicle.Id);
                var sortedWorkOrders = workOrders
                    .Where(wo => wo.Kilometers > 0)
                    .OrderBy(wo => wo.Kilometers)
                    .ToList();

                // Calculated data
                var dailyKilometersAverages = new List<double>();

                if (workOrders.Count < 2)
                {
                    continue;
                }

                for (int i = 0; i < sortedWorkOrders.Count; i++)
                {
                    if (i == 0)
                    {
                        continue;
                    }
                    var kilometerDifference = sortedWorkOrders[i].Kilometers - sortedWorkOrders[i - 1].Kilometers;
                    var daysDifference = (sortedWorkOrders[i].DateField.ToDateTime(TimeOnly.MinValue) - sortedWorkOrders[i - 1].DateField.ToDateTime(TimeOnly.MinValue)).TotalDays;

                    if (daysDifference > 0)
                    {
                        var dailyKilometersAverage = kilometerDifference / daysDifference;
                        dailyKilometersAverages.Add(dailyKilometersAverage);
                    }
                }

                double overallAverageDailyKilometers = 0;
                if (dailyKilometersAverages.Count > 0)
                {
                    overallAverageDailyKilometers = dailyKilometersAverages.Average();
                }

                var mostRecentOrder = workOrders.OrderBy(wo => wo.Fid).ToList().Last();
                var currentDate = DateOnly.FromDateTime(DateTime.Now);

                var daysSinceLastOilOrder = (currentDate.ToDateTime(TimeOnly.MinValue) - lastOilEngineWorkOrder.OrderFk.DateField.ToDateTime(TimeOnly.MinValue)).TotalDays;
                var daysSinceMostRecentOrder = (currentDate.ToDateTime(TimeOnly.MinValue) - mostRecentOrder.DateField.ToDateTime(TimeOnly.MinValue)).TotalDays;

                var estimatedCurrentKilometer = (overallAverageDailyKilometers * daysSinceMostRecentOrder) + mostRecentOrder.Kilometers;

                // This need to be implemented later, when maintenanceRegistries registers on the DB the frequency change of the maintenances
                // var useFulMaintenanceRegistries = allRegistries.OrderBy(mr => mr.Ki)

                if (daysSinceLastOilOrder > 26)
                {
                    var trackingData = new TrackingDataResponse
                    {
                        EstimatedCurrentKilometers = estimatedCurrentKilometer,
                        AverageKilometersPerDay = overallAverageDailyKilometers,
                        DaysSinceLastOrder = daysSinceLastOilOrder,
                        Customer = _mapper.Map<CustomerResponse>(vehicle.Customer),
                        Vehicle = _mapper.Map<VehicleResponse>(vehicle),
                        MileageLastOilOrder = lastOilEngineWorkOrder.Kilometers
                    };

                    response.Add(trackingData);
                }
            }

            return response.OrderBy(tdr => tdr.DaysSinceLastOrder).ToList();
        }
    }
}
