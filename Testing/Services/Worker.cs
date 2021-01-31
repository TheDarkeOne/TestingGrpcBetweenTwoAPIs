using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testing2.Services;
using static Testing2.Services.TelemetryCreationService;

namespace Testing.Services
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly IConfiguration config;
        public ObservableCollection<Telemetry> telemetries;
        public int TelemetryNumber = 0;
        protected TelemetryCreationServiceClient client = null;
        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }


        public ObservableCollection<Telemetry> Telemetries 
        {
            get 
            {
                if (telemetries == null) 
                {
                    telemetries = new ObservableCollection<Telemetry>();
                }
                return telemetries;
            }
        }

        protected TelemetryCreationServiceClient Client 
        {
            get 
            {
                if (client == null) 
                {
                    var channel = GrpcChannel.ForAddress(config["ServerUrl"]);
                    client = new TelemetryCreationServiceClient(channel);
                }

                return client;
            }
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) 
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                if (Telemetries.Count != 0) 
                {
                    foreach (var telemetry in Telemetries) 
                    {
                        TelemetryNumber++;
                        var pkt = new TelemetryRequest()
                        {
                            TelemetryNumber = $"{TelemetryNumber}",
                            TimeOfRequest = Timestamp.FromDateTime(telemetry.timeOfRequest),
                            WebRequestSent = Timestamp.FromDateTime(telemetry.webRequestSent),
                            WebRequestRecieved = Timestamp.FromDateTime(telemetry.webRequestRecieved),
                            StoreRequestStarted = Timestamp.FromDateTime(telemetry.StoreRequestStarted),
                            StoreRequestFinished = Timestamp.FromDateTime(telemetry.StoreRequestFinished),
                            Successful = true
                        };
                        var result = await Client.AddTelemetryAsync(pkt);
                        if (result.Success) 
                        {
                            logger.LogInformation("Telemetry Successfully Sent");
                        }

                        telemetries.Remove(telemetry);
                    }

                    
                    
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
