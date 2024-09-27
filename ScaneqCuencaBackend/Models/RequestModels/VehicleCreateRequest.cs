using ScaneqCuencaBackend.DBModels;

namespace ScaneqCuencaBackend.Models.RequestModels
{
    public class VehicleCreateRequest
    {
        public string Model { get; set; }
        public string Vin { get; set; }
        public int CustomerId { get; set; }
        public string Plate { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? Color { get; set; } = null!;
        public string? Engine { get; set; } = null!;
        public int? Year { get; set; } = null!;
        public string? Gearbox { get; set; } = null!;
        public string? AxleGear { get; set; } = null!;
        public decimal? RearAxleGearRatio { get; set; } = null!;

        public bool? MaintenanceAgreement { get; set; }
        public int? CooperativeId { get; set; }
        public int? FleetNumber { get; set; }
    }
}
