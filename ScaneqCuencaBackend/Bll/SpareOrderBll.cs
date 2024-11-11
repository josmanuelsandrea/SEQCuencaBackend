using AutoMapper;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class SpareOrderBll
    {
        private readonly SpareOrderRepository _SpareOrderR;
        private readonly SpareRegistryRepository _SpareRegistryR;
        private readonly SparePartRepository _SparePartR;
        private readonly BusOrderBll _busOrderB;
        private readonly CustomerBll _CustomerB;
        private readonly IMapper _mapper;
        private readonly SeqcuencabackendContext _db;
        public SpareOrderBll(SeqcuencabackendContext db, IMapper mapper)
        {
            _SpareOrderR = new(db);
            _SpareRegistryR = new(db);
            _SparePartR = new(db);
            _busOrderB = new(db, mapper);
            _CustomerB = new(db, mapper);
            _mapper = mapper;
            _db = db;
        }

        public SpareOrderResponseModel GetSpareOrderByBusOrderId(int busOrderId)
        {
            var orders = _SpareOrderR.GetAll().Where(order => order.BusOrderFk == busOrderId).ToList();
            return _mapper.Map<SpareOrderResponseModel>(orders);
        }

        public SpareOrderResponseModel GetSpareOrderByCustomerId(int customerId)
        {
            var orders = _SpareOrderR.GetAll().Where(order => order.BusOrderFk == customerId).ToList();
            return _mapper.Map<SpareOrderResponseModel>(orders);
        }

        public List<SpareOrderResponseModel> GetOpenOrders()
        {
            var orders = _SpareOrderR.GetAll().Where(order => order.Isclosed == false).ToList();
            return _mapper.Map<List<SpareOrderResponseModel>>(orders);
        }

        public List<SpareOrderResponseModel> GetClosedOrders()
        {
            var orders = _SpareOrderR.GetAll().Where(order => order.Isclosed == true).ToList();
            return _mapper.Map<List<SpareOrderResponseModel>>(orders);
        }

        public SpareOrderResponseModel? CloseOrder(int orderId)
        {
            var foundOrder = _SpareOrderR.GetById(orderId);
            if (foundOrder == null) { return null; }

            foundOrder.Isclosed = true;
            foundOrder.ClosedAt = DateTime.UtcNow;

            var result = _SpareOrderR.Update(foundOrder);

            if (result != null)
            {
                var mapResponse = _mapper.Map<SpareOrder, SpareOrderResponseModel>(result);
                return mapResponse;
            }

            return null;
        }

        public SpareOrderResponseModel? CreateOrder(SpareOrderRequest spareOrderR)
        {
            if (spareOrderR.BusOrderFk.HasValue)
            {
                // Check if the order exists
                var foundOrder = _busOrderB.GetWorkOrderById(spareOrderR.BusOrderFk.Value);
                if (foundOrder == null)
                {
                    return null;
                }
                // Check if already exists an order assigned to the workorder
                var existingOrder = _SpareOrderR.GetAll().ToList().Where(entity => entity.BusOrderFk == foundOrder.Id).FirstOrDefault();
                if (existingOrder != null)
                {
                    return null;
                }

                SpareOrder mapping = _mapper.Map<SpareOrderRequest, SpareOrder>(spareOrderR);
                mapping.Isclosed = false;

                var result = _SpareOrderR.Add(mapping);

                if (result == null)
                {
                    return null;
                }

                var mappingResult = _mapper.Map<SpareOrder, SpareOrderResponseModel>(result);
                return mappingResult;
            }

            if (spareOrderR.CustomerFk.HasValue)
            {
                
                var foundCustomer = _CustomerB.getCustomerById(spareOrderR.CustomerFk.Value);
                if (foundCustomer == null)
                {
                    return null;
                }

                SpareOrder mapping = _mapper.Map<SpareOrderRequest, SpareOrder>(spareOrderR);
                mapping.Isclosed = false;

                var result = _SpareOrderR.Add(mapping);

                if (result == null)
                {
                    return null;
                }

                var mappingResult = _mapper.Map<SpareOrder, SpareOrderResponseModel>(result);
                return mappingResult;
            }

            return null;
        }

        public SpareRegisterResponseModel? AddItemToOrder(SpareRegisterRequest spareRegisterR)
        {
            if (spareRegisterR.Quantity <= 0) { return null; }

            var foundSpare = _SparePartR.GetById(spareRegisterR.SpareFk);
            var foundOrder = _SpareOrderR.GetById(spareRegisterR.SpareOrderFk);

            if (foundSpare == null) { return null; }
            if (foundOrder == null) { return null; }

            var mapping = _mapper.Map<SpareRegisterRequest, SpareRegister>(spareRegisterR);

            var result = _SpareRegistryR.Add(mapping);
            if (result == null) { return null; }

            var response = _mapper.Map<SpareRegister, SpareRegisterResponseModel>(result);
            return response;
        }
    }
}
