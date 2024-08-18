using ScaneqCuencaBackend.DBModels;

namespace ScaneqCuencaBackend.Models.ResponseModels
{
    public class VehicleKilometerRangeResponse
    {
        public List<VehicleResponse> Vehicles { get; set; }
        public double Percentage { get; set; }
    }
}
