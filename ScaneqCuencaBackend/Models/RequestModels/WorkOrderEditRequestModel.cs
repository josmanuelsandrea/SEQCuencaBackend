namespace ScaneqCuencaBackend.Models.RequestModels
{
    public class WorkOrderEditRequestModel
    {
        public int Fid { get; set; }
        public DateOnly DateField { get; set; }
        public int CustomerId { get; set; }
        public string? Description { get; set; }
        public bool Iswarranty { get; set; }
        public int Kilometers { get; set; }
        public bool Isarchived { get; set; }
        public int Storedvolume { get; set; }
        public int? VehicleId { get; set; }
        public required List<MaintenanceRegistryRequest> maintenances { get; set; }
    }
}
