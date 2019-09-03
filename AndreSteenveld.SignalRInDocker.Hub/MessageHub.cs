using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace AndreSteenveld.SignalRInDocker.MessageHub
{
	public class Messager
	{

		private readonly IHubContext<MessageHub> Hub;

		public Messager(IHubContext<MessageHub> hub)
		{

			Hub = hub;

		}

		private async Task BroadcastMessage(string message)
		{

			await Hub.Clients.All.SendAsync("ReceiveMessage", message);

		}

		public async Task SendMessage(string message)
		{

			await BroadcastMessage(message);

		}

	}

	public class MessageHub : Hub
	{

		private readonly Messager Messager;

		public MessageHub(Messager messager)
		{

			Messager = messager;

		}

		public async Task SendMessage(string message)
		{

			await Messager.SendMessage(message);

		}

	}

}
