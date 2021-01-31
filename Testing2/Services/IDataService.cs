using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testing2.Services
{
    public interface IDataService
    {
        Task CreateTelemetry(Telemetry telemetry);
        IEnumerable<Telemetry> Telemetries();
        Telemetry Telemetry(int id);
    }
}
