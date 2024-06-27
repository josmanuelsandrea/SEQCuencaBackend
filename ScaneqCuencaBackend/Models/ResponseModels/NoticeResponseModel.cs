using ScaneqCuencaBackend.DBModels;

namespace ScaneqCuencaBackend.Models.ResponseModels
{
    public class NoticeResponseModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateOnly NoticeDate { get; set; }
        public string? Description { get; set; }
        public string? Severity { get; set; }
        public bool Resolved { get; set; }
    }
}
