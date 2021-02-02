using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Protos;

namespace Testing.Services
{
    public class UserService : UserCreation.UserCreationBase
    {
        private readonly IDataService dataService;
        private readonly gRPCClass gRPC;

        public UserService(IDataService dataService, gRPCClass gRPC)
        {
            this.dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            this.gRPC = gRPC ?? throw new ArgumentNullException(nameof(gRPC));
        }

        public override async Task<UserResponse> CreateUser(UserRequest request, ServerCallContext context)
        {
            var webRequestRecieved = DateTime.UtcNow;
            var result = new UserResponse()
            {
                Success = true
            };

            if (request.Successful == true)
            {
                var users = new Users()
                {
                    Username = request.Username,
                    Password = request.Password,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address
                };

                var newTelemetry = new Telemetry()
                {
                    TelemetryNumber = request.RequestNumber,
                    timeOfRequest = DateTime.UtcNow,
                    webRequestSent = request.WebRequestSent.ToDateTime(),
                    webRequestRecieved = webRequestRecieved,
                    successful = request.Successful
                };

                newTelemetry.StoreRequestStarted = DateTime.UtcNow;
                await dataService.CreateUsers(users);
                newTelemetry.StoreRequestFinished = DateTime.UtcNow;

                await gRPC.CallServer(newTelemetry);
            }

            return result;
        }
    }
}
