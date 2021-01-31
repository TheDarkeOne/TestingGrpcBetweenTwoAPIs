using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testing2.Services
{
    public class TelemetryService : TelemetryCreationService.TelemetryCreationServiceBase
    {
        private readonly IDataService dataService;

        public TelemetryService(IDataService dataService)
        {
            this.dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        public override async Task<TelemetryResponse> AddTelemetry(TelemetryRequest request, ServerCallContext context)
        {
            var result = new TelemetryResponse()
            {
                Success = false
            };
         
            if (request.Successful == true) 
            {
                var newTelemetry = new Telemetry()
                {
                    TelemetryNumber = request.TelemetryNumber,
                    timeOfRequest = request.TimeOfRequest.ToDateTime(),
                    webRequestSent = request.WebRequestSent.ToDateTime(),
                    webRequestRecieved = request.WebRequestRecieved.ToDateTime(),
                    StoreRequestStarted = request.StoreRequestStarted.ToDateTime(),
                    StoreRequestFinished = request.StoreRequestFinished.ToDateTime()
                };

                await dataService.CreateTelemetry(newTelemetry);
            }

            return result;
        }
    }
}
