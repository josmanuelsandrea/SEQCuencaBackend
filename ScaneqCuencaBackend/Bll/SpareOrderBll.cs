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
       
        public SpareOrderBll(IMapper mapper, BusOrderBll busOrderB, CustomerBll customerB, SpareOrderRepository spareOrderR, SpareRegistryRepository spareRegistryR = null, SparePartRepository sparePartR = null)
        {

            _mapper = mapper;
            _busOrderB = busOrderB;
            _CustomerB = customerB;
            _SpareOrderR = spareOrderR;
            _SpareRegistryR = spareRegistryR;
            _SparePartR = sparePartR;
        }

        public SpareOrderResponseModel? GetSpareOrderByBusOrderId(int busOrderId)
        {
            var orders = _SpareOrderR.GetAll().Where(order => order.BusOrderFk == busOrderId).ToList().FirstOrDefault();
            if (orders != null)
            {
                return _mapper.Map<SpareOrderResponseModel>(orders);
            }

            return null;
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
                if (foundOrder.Data == null)
                {
                    return null;
                }
                // Check if already exists an order assigned to the workorder
                var existingOrder = _SpareOrderR.GetAll().ToList().Where(entity => entity.BusOrderFk == foundOrder.Data.Id).FirstOrDefault();
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
            // If 0 is received return null
            if (spareRegisterR.Quantity <= 0) { return null; }

            var foundSpare = _SparePartR.GetById(spareRegisterR.SpareFk);
            var foundOrder = _SpareOrderR.GetById(spareRegisterR.SpareOrderFk);

            // If the sparePart was not found return null
            if (foundSpare == null) { return null; }

            // If the order was not found return null
            if (foundOrder == null) { return null; }
            // Check if the spare part was already added to the order
            List<SpareRegister> spareRegisters = _SpareRegistryR.GetAll();
            // Filter using Where
            var foundSpareRegister = spareRegisters
                .Where(spareR => spareR.SpareFk == foundSpare.Id && spareR.SpareOrderFk == foundOrder.Id)
                .ToList();
            
            // No coincidence spareRegister case
            if (!foundSpareRegister.Any())
            {
                var mapping = _mapper.Map<SpareRegisterRequest, SpareRegister>(spareRegisterR);

                var result = _SpareRegistryR.Add(mapping);
                if (result == null) { return null; }

                var response = _mapper.Map<SpareRegister, SpareRegisterResponseModel>(result);
                return response;
            }
            // Coincidence spareRegister case
            else
            {
                var result = _SpareRegistryR.SumToQuantity(foundSpareRegister.First().Id, 1);
                if (result == null) { return null; };

                return _mapper.Map<SpareRegister, SpareRegisterResponseModel>(result);
            }
        }

        public SpareRegisterResponseModel? SubstractItemToOrder(SpareRegisterRequest spareRegisterR, int quantity)
        {
            // If 0 is received return null
            if (spareRegisterR.Quantity <= 0) { return null; }

            var foundSpare = _SparePartR.GetById(spareRegisterR.SpareFk);
            var foundOrder = _SpareOrderR.GetById(spareRegisterR.SpareOrderFk);

            // If the sparePart was not found return null
            if (foundSpare == null) { return null; }

            // If the order was not found return null
            if (foundOrder == null) { return null; }
            // Check if the spare part was already added to the order
            List<SpareRegister> spareRegisters = _SpareRegistryR.GetAll();
            var foundSpareRegister = spareRegisters
                .Where(spareR => spareR.SpareFk == foundSpare.Id && spareR.SpareOrderFk == foundOrder.Id)
                .ToList();

            // No coincidence spareRegister case
            if (!foundSpareRegister.Any())
            {
                var mapping = _mapper.Map<SpareRegisterRequest, SpareRegister>(spareRegisterR);

                var result = _SpareRegistryR.Add(mapping);
                if (result == null) { return null; }

                var response = _mapper.Map<SpareRegister, SpareRegisterResponseModel>(result);
                return response;
            }
            // Coincidence spareRegister case
            else
            {
                var result = _SpareRegistryR.SubstractToQuantity(foundSpareRegister.First().Id, quantity);
                if (result == null) { return null; };

                return _mapper.Map<SpareRegister, SpareRegisterResponseModel>(result);
            }
        }

        public SparePartResponseModel? CreateSparePart(SpareRequest request)
        {
            // Check first is there any spare with the same code:
            var foundSpareWithGivenCode = _SparePartR.GetByCode(request.Code);
            if (foundSpareWithGivenCode != null) { return null; }

            // Mapp result
            var mappingRequest = _mapper.Map<SparePart>(request);
            var result = _SparePartR.Add(mappingRequest);
            if (result == null) { return null; }

            var mappingResponse = _mapper.Map<SparePartResponseModel>(result);
            return mappingResponse;
        }
    }
}
