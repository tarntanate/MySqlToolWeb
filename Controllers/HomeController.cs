using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using PingToolWeb.Models;

namespace PingToolWeb.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			var model = new IndexViewModel { error = string.Empty };
			return View(model);
		}

		[HttpPost]
		public IActionResult Index([FromForm] string serverName, [FromForm] string databaseName, [FromForm] string username, [FromForm] string password)
		{
			string connetionString = null;
			string resultMessage = string.Empty;
			MySqlConnection cnn;
			connetionString = $"server={serverName};database={databaseName};uid={username};pwd={password};";
			cnn = new MySqlConnection(connetionString);
			try
			{
				cnn.Open();
				resultMessage = "Connection successful..";
				cnn.Close();
			}
			catch (Exception ex)
			{
				resultMessage = "Error: " + ex.Message;
			}

			var model = new IndexViewModel { 
				serverName = serverName, 
				databaseName = databaseName,
				username = username,
				password = password,
				result = resultMessage
			};
			return View(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
