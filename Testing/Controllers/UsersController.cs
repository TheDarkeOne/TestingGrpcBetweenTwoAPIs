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


        public UsersController(ILogger<UsersController> logger, IDataService dataService)
        {
            _logger = logger;
            this.dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
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
            newTelemetry.webRequestSent = DateTime.MinValue;
            newTelemetry.webRequestRecieved = DateTime.Now;
            
            var newUser = new Users()
            {
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Password = "johndoe1234",
                Address = "1234 fake street N 1543 imposter lane E"
            };

            newTelemetry.StoreRequestStarted = DateTime.Now;
            await dataService.CreateUsers(newUser);
            newTelemetry.StoreRequestFinished = DateTime.Now;

            var json = JsonConvert.SerializeObject(newTelemetry);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:5000/Telemetry/AddTelemetry";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Result is:" + result);
        }

        [HttpPost]
        public async Task AddUser(Users user)
        {
            await dataService.CreateUsers(user);
        }
    }
}
