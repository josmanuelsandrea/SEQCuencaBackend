namespace ScaneqCuencaBackend.Models.ResponseModels
{
    public class SpareOrderResponseModel
    {
        public int Id { get; set; }
        public WorkOrderResponseModel? BusOrder { get; set; }
        public CustomerResponse? Customer { get; set; }
        public bool Isclosed { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public List<SpareRegisterResponseModel> SpareRegisters { get; set; }
    }
}
