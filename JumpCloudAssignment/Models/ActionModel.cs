using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpCloudAssignment.Models
{
	public class ActionModel
	{
		[JsonProperty("action", Required = Required.Always)]
		public string Action { get; set; }
		[JsonProperty("time", Required = Required.Always)]
		public int Time { get; set; }
	}
}
