using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Testing.Services;

namespace Testing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IDataService dataService;
        private readonly gRPCClass gRPC;


        public UsersController(ILogger<UsersController> logger, IDataService dataService, gRPCClass gRPC)
        {
            _logger = logger;
            this.dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            this.gRPC = gRPC;
        }

        [HttpGet]
        public IEnumerable<Users> Get()
        {
            return dataService.Users();
        }

        [HttpGet("[action]")]
        public async Task GetDefaultUser()
        {
            var newTelemetry = new Telemetry();

            newTelemetry.TelemetryNumber = "2";
            newTelemetry.timeOfRequest = DateTime.UtcNow;
            newTelemetry.webRequestSent = DateTime.UtcNow;
            newTelemetry.webRequestRecieved = DateTime.UtcNow;
            
            var newUser = new Users()
            {
                FirstName = "Jane",
                LastName = "Doe",
                Username = "johndoe",
                Password = "johndoe1234",
                Address = "1234 fake street N 1543 imposter lane E"
            };

            newTelemetry.StoreRequestStarted = DateTime.UtcNow;
            await dataService.CreateUsers(newUser);
            newTelemetry.StoreRequestFinished = DateTime.UtcNow;

            await gRPC.CallServer(newTelemetry);
        }

        [HttpPost]
        public async Task AddUser(Users user)
        {
            await dataService.CreateUsers(user);
        }
    }
}
