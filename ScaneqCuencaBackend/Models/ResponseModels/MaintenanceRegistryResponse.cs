namespace ScaneqCuencaBackend.Models.ResponseModels
{
    public class MaintenanceRegistryResponse
    {
        public int Id { get; set; }
        public int VehicleFkId { get; set; }
        public int OrderFkId { get; set; }
        public DateOnly MaintenanceDate { get; set; }
        public string MaintenanceType { get; set; } = null!;
        public string? Description { get; set; }
        public int Kilometers { get; set; }
    }
}
