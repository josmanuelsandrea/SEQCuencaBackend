namespace ScaneqCuencaBackend.Models.RequestModels
{
    public class PDFRequestModels
    {
        public partial class PDFWorkOrderRequest
        {
            public Data Data { get; set; }
        }

        public partial class Data
        {
            public string? WorkOrderId { get; set; }
            public string? Date { get; set; }
            public string? Customer { get; set; }
            public string? Plate { get; set; }
            public string? Model { get; set; }
            public string? Chassis { get; set; }
            public string? Description { get; set; }
            public List<SpareList>? SpareList { get; set; }
        }

        public partial class SpareList
        {
            public string? Code { get; set; }
            public string? Description { get; set; }
            public string? Quantity { get; set; }
        }
    }
}
