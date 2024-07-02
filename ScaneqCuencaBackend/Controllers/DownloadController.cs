using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScaneqCuencaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly SeqcuencabackendContext _db;
        private readonly BusOrdersRepository _BusOrderR;
        public DownloadController(SeqcuencabackendContext db)
        {
            _db = db;
            _BusOrderR = new BusOrdersRepository(db);
        }
        // GET: api/<ValuesController>
        [HttpGet("BusOrders")]
        public async Task<IActionResult> Get()
        {
            var data = await _BusOrderR.GetOrdersAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");

                worksheet.Cell(1, 1).Value = "Fid";
                worksheet.Cell(1, 2).Value = "Customer";
                worksheet.Cell(1, 3).Value = "Vehicle";
                worksheet.Cell(1, 4).Value = "DateField";
                worksheet.Cell(1, 5).Value = "Description";
                worksheet.Cell(1, 6).Value = "Kilometers";
                worksheet.Cell(1, 7).Value = "Iswarranty";
                worksheet.Cell(1, 8).Value = "Storedvolume";

                for (int i = 0; i < data.Count; i++)
                {
                    var order = data[i];
                    var fid = order.Fid;
                    var customerName = order.Customer?.Name ?? "unknown";
                    var vehicle = order.Vehicle?.Plate ?? "generic";
                    var dateField = order.DateField.ToString() ?? "No Date";
                    var description = order.Description ?? "No Description";
                    var kilometers = order.Kilometers;
                    var isWarranty = order.Iswarranty;
                    var storedVolume = order.Storedvolume;

                    worksheet.Cell(i + 2, 1).Value = fid;
                    worksheet.Cell(i + 2, 2).Value = customerName;
                    worksheet.Cell(i + 2, 3).Value = vehicle;
                    worksheet.Cell(i + 2, 4).Value = dateField;
                    worksheet.Cell(i + 2, 5).Value = description;
                    worksheet.Cell(i + 2, 6).Value = kilometers;
                    worksheet.Cell(i + 2, 7).Value = isWarranty;
                    worksheet.Cell(i + 2, 8).Value = storedVolume;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Data.xlsx");
                }
            }
        }
    }
}
