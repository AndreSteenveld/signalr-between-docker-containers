using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AndreSteenveld.SignalRInDocker.Receiver
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

			Console.WriteLine("Starting connection. Press Ctrl-C to close.");

			var cancel = new CancellationTokenSource();
			Console.CancelKeyPress += (s, a) =>
			{

				a.Cancel = true;
				cancel.Cancel();

			};

			var channel = await connection.StreamAsChannelAsync<string>("ReceiveMessage", CancellationToken.None);

			while (false == cancel.IsCancellationRequested && await channel.WaitToReadAsync())
				while (channel.TryRead(out var message))
					Console.WriteLine(message);

		}
	}
}
