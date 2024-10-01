using ScaneqCuencaBackend.DBModels;

namespace ScaneqCuencaBackend.Models.RequestModels
{
    public class VehicleEditRequest
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public string? Vin { get; set; }
        public string? Color { get; set; }
        public string? Engine { get; set; }
        public int? Year { get; set; }
        public string? Gearbox { get; set; }
        public string? AxleGear { get; set; }
        public decimal? RearAxleGearRatio { get; set; }
        public string Plate { get; set; } = null!;
        public string? Type { get; set; }
        public int? FleetNumber { get; set; } = null!;
        public bool? MaintenanceAgreement { get; set; }
        public int? CooperativeId { get; set; }
    }
}
