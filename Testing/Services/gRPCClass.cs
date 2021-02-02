using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Testing2.Services;

namespace Testing.Services
{
    public class gRPCClass
    {
        public gRPCClass()
        {

        }

        public async Task CallServer(Telemetry telemetry) 
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var Client = new TelemetryCreation.TelemetryCreationClient(channel);

            var pkt = new TelemetryRequest()
            {
                TelemetryNumber = "1",
                TimeOfRequest = Timestamp.FromDateTime(telemetry.timeOfRequest),
                WebRequestSent = Timestamp.FromDateTime(telemetry.webRequestSent),
                WebRequestRecieved = Timestamp.FromDateTime(telemetry.webRequestRecieved),
                StoreRequestStarted = Timestamp.FromDateTime(telemetry.StoreRequestStarted),
                StoreRequestFinished = Timestamp.FromDateTime(telemetry.StoreRequestFinished),
                Successful = true
            };
            var result = await Client.AddTelemetryAsync(pkt);

            if (result.Success == true) 
            {
                Console.WriteLine("You created a successful call with the gRPC service to the Telemetry API");
            }
        } 
    }
}
