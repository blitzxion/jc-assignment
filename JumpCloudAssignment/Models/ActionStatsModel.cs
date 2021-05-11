using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpCloudAssignment.Models
{
	public class ActionStatsModel
	{
		[JsonProperty("action")]
		public string Action { get; set; }
		[JsonProperty("avg")]
		public double Avg { get; set; }
	}
}
