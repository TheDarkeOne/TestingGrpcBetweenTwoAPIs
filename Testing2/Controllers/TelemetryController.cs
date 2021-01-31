using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Testing2.Services;

namespace Testing2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelemetryController : ControllerBase
    {
        private readonly ILogger<TelemetryController> _logger;
        private readonly IDataService dataService;

        public TelemetryController(ILogger<TelemetryController> logger, IDataService dataService)
        {
            _logger = logger;
            this.dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        [HttpGet]
        public IEnumerable<Telemetry> Get()
        {
            return dataService.Telemetries();
        }

        [HttpGet("[action]")]
        public async Task GetDefaultTelemetry()
        {
            var newTelemetry = new Telemetry()
            {
                TelemetryNumber = "1",
                timeOfRequest = DateTime.Now,
                webRequestSent = DateTime.Now,
                webRequestRecieved = DateTime.Now,
                StoreRequestStarted = DateTime.Now,
                StoreRequestFinished = DateTime.Now
            };

            await dataService.CreateTelemetry(newTelemetry);
        }

        [HttpPost]
        public void AddTelemetry(Telemetry telemetry) 
        {
            dataService.CreateTelemetry(telemetry);
        }

    }
}
