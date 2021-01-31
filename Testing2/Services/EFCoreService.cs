using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testing2.Services
{
    public class EFCoreService : IDataService
    {
        private readonly ApplicationDbContext context;

        public EFCoreService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task CreateTelemetry(Telemetry telemetry)
        {
            context.Telemetry.Add(telemetry);
            await context.SaveChangesAsync();
        }

        public IEnumerable<Telemetry> Telemetries()
        {
            var list = context.Telemetry.ToList();
            return list;
        }

        public Telemetry Telemetry(int id)
        {
            return context.Telemetry.Find(id);
        }
    }
}
