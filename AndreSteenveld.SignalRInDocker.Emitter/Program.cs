using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AndreSteenveld.SignalRInDocker.Emitter
{
	class Program
	{

		static async Task Main(string[] args)
		{

			var url = args[0];
			var connection = new HubConnectionBuilder()
				.WithUrl(url)
				.ConfigureLogging(logging => logging.AddConsole())
				.AddMessagePackProtocol()
				.Build();

			await connection.StartAsync();

			TimerCallback action = (state) => {

				connection.InvokeAsync("SendMessage", $"Message: ${ DateTime.Now.ToLocalTime() }");

			};

			var timer = new Timer(action, null, 1000, 1000);

			Console.WriteLine("Starting connection. Press Ctrl-C to close.");

			var cancel = new CancellationTokenSource();
			Console.CancelKeyPress += (s, a) =>
			{

				timer.Dispose();

				a.Cancel = true;
				cancel.Cancel();

			};

		}

	}
}
