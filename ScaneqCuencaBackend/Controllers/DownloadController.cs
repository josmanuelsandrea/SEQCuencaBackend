using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.Bll;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScaneqCuencaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly BusOrdersRepository _BusOrderR;
        private readonly NoticeBll _NoticeB;
        public DownloadController(NoticeBll noticeB, BusOrdersRepository busOrderR)
        {
            _NoticeB = noticeB;
            _BusOrderR = busOrderR;
        }
        // GET: api/<ValuesController>
        [HttpGet("BusOrders")]
        public async Task<IActionResult> DownloadWorkOrders()
        {
            var data = await _BusOrderR.GetOrdersAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");

                worksheet.Cell(1, 1).Value = "Numero de orden";
                worksheet.Cell(1, 2).Value = "Cliente";
                worksheet.Cell(1, 3).Value = "Placa";
                worksheet.Cell(1, 4).Value = "Fecha";
                worksheet.Cell(1, 5).Value = "Descripcion";
                worksheet.Cell(1, 6).Value = "Kilometraje";
                worksheet.Cell(1, 7).Value = "Garantia";
                worksheet.Cell(1, 8).Value = "Volumen archivo";

                for (int i = 0; i < data.Count; i++)
                {
                    var order = data[i];
                    var fid = order.Fid;
                    var customerName = order.Customer?.Name ?? "unknown";
                    var vehicle = order.Vehicle?.Plate ?? "generic";
                    var dateField = order.DateField.ToString("yyyy/MM/dd") ?? "No Date";
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
        [HttpGet("VehicleReport/{id}")]
        public async Task<IActionResult> DownloadVehicleOrders(int id)
        {
            var data = await _BusOrderR.GetWorkOrderByVehicleIdAsync(id);
            var alertsData = await _NoticeB.GetNoticesByVehicleIdAsync(id);

            if (data.Count == 0)
            {
                return BadRequest(new { message = "No work orders were found"});
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("WorkOrders");
                var worksheet2 = workbook.Worksheets.Add("Alerts");

                worksheet.Cell(1, 1).Value = "Numero de orden";
                worksheet.Cell(1, 2).Value = "Cliente";
                worksheet.Cell(1, 3).Value = "Placa";
                worksheet.Cell(1, 4).Value = "Fecha";
                worksheet.Cell(1, 5).Value = "Descripcion";
                worksheet.Cell(1, 6).Value = "Kilometraje";
                worksheet.Cell(1, 7).Value = "Garantia";
                worksheet.Cell(1, 8).Value = "Volumen archivo";

                worksheet2.Cell(1, 1).Value = "Fecha";
                worksheet2.Cell(1, 2).Value = "Descripcion";
                worksheet2.Cell(1, 3).Value = "Gravedad";
                worksheet2.Cell(1, 4).Value = "Resuelto";

                for (int i = 0; i < data.Count; i++)
                {
                    var order = data[i];
                    var fid = order.Fid;
                    var customerName = order.Customer?.Name ?? "unknown";
                    var vehicle = order.Vehicle?.Plate ?? "generic";
                    var dateField = order.DateField.ToString("dd/MM/yyyy") ?? "No Date";
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

                for (int i = 0; i < alertsData.Count; i++)
                {
                    worksheet2.Cell(i + 2, 1).Value = alertsData[i].NoticeDate.ToString();
                    worksheet2.Cell(i + 2, 2).Value = alertsData[i].Description;
                    worksheet2.Cell(i + 2, 3).Value = alertsData[i].Severity;
                    worksheet2.Cell(i + 2, 4).Value = alertsData[i].Resolved;
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
