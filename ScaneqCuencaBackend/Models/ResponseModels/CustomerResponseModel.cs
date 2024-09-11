namespace ScaneqCuencaBackend.Models.ResponseModels
{
    public class CustomerResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? IdRucNumber { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
