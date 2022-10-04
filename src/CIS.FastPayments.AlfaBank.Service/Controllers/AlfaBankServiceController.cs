using CIS.FastPayments.AlfaBank.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace CIS.FastPayments.AlfaBank.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlfaBankServiceController : ControllerBase
    {
        private readonly ILogger<AlfaBankServiceController> _logger;

        public AlfaBankServiceController(ILogger<AlfaBankServiceController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        //[Consumes(DefaultSettings.ContentType)]
        public async Task<QRCodeReport> CreateQRCodeReport(QRCodeReport reportModel)
        {
            _logger.LogInformation($"{nameof(CreateQRCodeReport)}, QrcId = <{reportModel.QrcId}>.");
            //var fiscalRequest = new FiscalRequest
            //{
            //    SaleSlipId = Guid.Parse(reportModel.ExternalId),
            //    Uuid = reportModel.Uuid,
            //    Timestamp = DateTime.UtcNow,
            //    OriginalReportModel = reportModel
            //};

            //await _fiscalRequestService.CreateAsync(fiscalRequest);

            return reportModel;
        }
    }
}