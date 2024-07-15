namespace ScaneqCuencaBackend.Models.RequestModels
{
    public class MaintenanceRegistryRequest
    {
        public int VehicleFkId { get; set; }
        public int? OrderFkId { get; set; }
        public DateOnly MaintenanceDate { get; set; }
        public string MaintenanceType { get; set; } = null!;
        public string? Description { get; set; }
        public int Kilometers { get; set; }
    }
}
