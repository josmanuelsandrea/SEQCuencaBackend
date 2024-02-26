using ScaneqCuencaBackend.Bll;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.ResponseModels;

namespace ScaneqCuencaBackend.Helpers
{
    public static class WorkOrderMapper
    {
        public static WorkOrderResponseModel ModelMapper(WorkOrder input)
        {
            WorkOrderResponseModel response = new WorkOrderResponseModel()
            {
                Uid = input.Uid,
                Fid = input.Fid,
                Billquantity = input.Billquantity,
                DateField = input.DateField,
                Description = input.Description,
                Isarchived = input.Isarchived,
                Iswarranty = input.Iswarranty,
                Kilometers = input.Kilometers,
                Labourcost = input.Labourcost,
                Storedvolume = input.Storedvolume,
                Customer = CustomerBll.getCustomerById(input.CustomerId),
            };

            return response;
        }
        public static List<WorkOrderResponseModel> ListModelMapper(List<WorkOrder> input)
        {
            List<WorkOrderResponseModel> response = new List<WorkOrderResponseModel>();
            foreach (WorkOrder item in input)
            {
                response.Add(new WorkOrderResponseModel()
                {
                    Uid = item.Uid,
                    Fid = item.Fid,
                    Billquantity = item.Billquantity,
                    DateField = item.DateField,
                    Description = item.Description,
                    Isarchived = item.Isarchived,
                    Iswarranty = item.Iswarranty,
                    Kilometers = item.Kilometers,
                    Labourcost = item.Labourcost,
                    Storedvolume = item.Storedvolume,
                    Customer = CustomerBll.getCustomerById(item.CustomerId),
                });
            }

            return response;
        }
    }
}
