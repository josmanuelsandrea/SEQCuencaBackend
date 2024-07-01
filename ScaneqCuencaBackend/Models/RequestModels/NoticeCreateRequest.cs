using ScaneqCuencaBackend.DBModels;

namespace ScaneqCuencaBackend.Models.RequestModels
{
    public class NoticeCreateRequest
    {
        public int VehicleId { get; set; }
        public DateOnly NoticeDate { get; set; }
        public string? Description { get; set; }
        public string? Severity { get; set; }
    }

    public class NoticeUpdateRequest
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Severity { get; set; }
        public bool Resolved { get; set; }

    }
}
