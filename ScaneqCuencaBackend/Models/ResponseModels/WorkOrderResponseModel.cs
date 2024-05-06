using ScaneqCuencaBackend.DBModels;

namespace ScaneqCuencaBackend.Models.ResponseModels
{
    public class WorkOrderResponseModel
    {
        public int Uid { get; set; }
        public int Fid { get; set; }
        public DateOnly DateField { get; set; }
        public int Customer { get; set; }
        public string? Description { get; set; }
        public decimal Billquantity { get; set; }
        public decimal Labourcost { get; set; }
        public bool Iswarranty { get; set; }
        public int Kilometers { get; set; }
        public bool Isarchived { get; set; }
        public int Storedvolume { get; set; }
    }
}
