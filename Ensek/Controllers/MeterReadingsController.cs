using Microsoft.AspNetCore.Mvc;
using Services;

namespace Ensek.Controllers
{
    [ApiController]
    [Route("meter-reading-uploads")]
    public class MeterReadingsController : ControllerBase
    {
        private readonly IMeterReadingService _meterReadingService;

        /// <summary>
        /// Dependency Injection
        /// </summary>
        /// <param name="meterReadingService"></param>
        public MeterReadingsController(IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService;
        }

        /// <summary>
        /// Post endpoint used to upload all meter readings present in the uploaded csv file
        /// </summary>
        /// <param name="csv"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile csv)
        {
            var result = await _meterReadingService.SaveOrUpdate(csv);

            return Ok(result);
        }
    }
}