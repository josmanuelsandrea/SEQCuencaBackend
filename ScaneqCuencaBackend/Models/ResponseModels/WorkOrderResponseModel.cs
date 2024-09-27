namespace ScaneqCuencaBackend.Models.ResponseModels
{
    public class WorkOrderResponseModel
    {
        public int Id { get; set; }
        public int Fid { get; set; }
        public DateOnly DateField { get; set; }
        public CustomerResponse? Customer { get; set; }
        public string? Description { get; set; }
        public bool Iswarranty { get; set; }
        public int Kilometers { get; set; }
        public bool Isarchived { get; set; }
        public int Storedvolume { get; set; }
        public VehicleResponse? Vehicle {  get; set; }
    }

    

    public class CustomerResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
