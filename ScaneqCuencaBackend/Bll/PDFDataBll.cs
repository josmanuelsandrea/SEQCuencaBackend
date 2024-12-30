using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class PDFDataBll
    {
        private readonly BusOrdersRepository _busOrderRepository;
        private readonly Lazy<SpareOrderBll> _spareOrderBll;

        public PDFDataBll(BusOrdersRepository busOrderRepository, SpareOrderRepository spareOrderRepository, SpareRegistryRepository spareRegistryRepository, Lazy<SpareOrderBll> spareOrderBll)
        {
            _busOrderRepository = busOrderRepository;
            _spareOrderBll = spareOrderBll;
        }

        public PDFRequestModels.PDFWorkOrderRequest? PDFWorkOrderData(int workOrderId)
        {
            var workOrder = _busOrderRepository.GetWorkOrderByNumber(workOrderId);
            if (workOrder == null) { return null; }

            var SpareRegister = _spareOrderBll.Value.GetSpareOrderByBusOrderId(workOrder.Id);

            List<PDFRequestModels.SpareList> spareLists = new List<PDFRequestModels.SpareList>();
            if (SpareRegister != null)
            {
                if (SpareRegister.SpareRegisters.Count > 0)
                {
                    foreach(var spare in SpareRegister.SpareRegisters)
                    {
                        spareLists.Add(new PDFRequestModels.SpareList
                        {
                            Code = spare.SparePart?.Code.ToString() ?? string.Empty,
                            Description = spare.SparePart?.Name ?? string.Empty,
                            Quantity = spare.Quantity.ToString(),
                        });
                    }
                }
            }


            var response = new PDFRequestModels.PDFWorkOrderRequest {
                Data = new PDFRequestModels.Data
                {
                    Chassis = workOrder.Vehicle?.Vin?.ToString() ?? string.Empty,
                    Customer = workOrder.Customer?.Name ?? string.Empty,
                    Date = workOrder.DateField.ToString("yyyy-MM-dd") ?? string.Empty,
                    Description = workOrder.Description ?? string.Empty,
                    Model = workOrder.Vehicle?.Model ?? string.Empty,
                    Plate = workOrder.Vehicle?.Plate ?? string.Empty,
                    WorkOrderId = workOrder.Fid.ToString() ?? string.Empty,
                    SpareList = spareLists,
                    Kilometers = workOrder.Kilometers.ToString() ?? string.Empty,
                    Guarantee = workOrder.Iswarranty
                }
            };

            return response;
        }
    }
}
