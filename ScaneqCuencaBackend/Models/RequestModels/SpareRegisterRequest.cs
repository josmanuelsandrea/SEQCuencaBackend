namespace ScaneqCuencaBackend.Models.RequestModels
{
    public class SpareRegisterRequest
    {
        public int SpareOrderFk { get; set; }
        public int SpareFk { get; set; }
        public int Quantity { get; set; }
    }
}
