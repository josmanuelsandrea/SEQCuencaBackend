namespace ScaneqCuencaBackend.Models.ResponseModels
{
    public class TrackingDataResponse
    {
        public double EstimatedCurrentKilometers { get; set; }
        public VehicleResponse Vehicle { get; set; }
        public CustomerResponse Customer { get; set; }
        public double DaysSinceLastOrder { get; set; }
        public double AverageKilometersPerDay { get; set; }
        public double MileageLastOilOrder { get; set; }
        //public List<MaintenanceRegistryResponse> PendingMaintenances { get; set;}
    }
}
