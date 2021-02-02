using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testing2
{
    public class Telemetry
    {
		public int Id { get; set; }
		public string TelemetryNumber { get; set; }
		public DateTime timeOfRequest { get; set; }
		public DateTime webRequestSent { get; set; }
		public DateTime webRequestRecieved { get; set; }
		public DateTime StoreRequestStarted { get; set; }
		public DateTime StoreRequestFinished { get; set; }
		public bool successful { get; set; }
	}
}
