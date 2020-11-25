using System;

namespace PingToolWeb.Models
{
	public class IndexViewModel
	{
		public string serverName { get; set; }
		public string databaseName { get; set; }
		public string username { get; set; }
		public string password { get; set; }
		public string error { get; set; }
		public string result { get; set; }

	}
}