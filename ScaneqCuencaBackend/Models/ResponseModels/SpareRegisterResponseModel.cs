using ScaneqCuencaBackend.DBModels;

namespace ScaneqCuencaBackend.Models.ResponseModels
{
    public class SpareRegisterResponseModel
    {
        public int Id { get; set; }
        public int SpareOrderFk { get; set; }
        public int SpareFk { get; set; }
        public int Quantity { get; set; }
        public DateTime? AddedAt { get; set; }
        public SparePartResponseModel? SparePart { get; set; }
    }
}
