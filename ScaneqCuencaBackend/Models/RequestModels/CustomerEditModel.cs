namespace ScaneqCuencaBackend.Models.RequestModels
{
    public class CustomerEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? IdRucNumber { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
