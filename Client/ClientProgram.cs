using System;
using System.Threading.Tasks;
using NetMQ;

namespace Client
{
	class ClientProgram
	{
		static void Main(string[] args)
		{
			using (NetMQContext context = NetMQContext.Create())
			using (NetMQSocket subscribeSocket = context.CreateSubscriberSocket())
			{
				subscribeSocket.Connect("tcp://127.0.0.1:5002");
				subscribeSocket.ReceiveReady += SubSocketOnReceiveReady;
				subscribeSocket.Subscribe(""); //Prefix of messages to receive. Empty string receives all messages
				Poller poller = new Poller();
				poller.AddSocket(subscribeSocket);
				Task.Factory.StartNew(poller.Start);
				Console.WriteLine("Waiting to receive messages.  Press 'q' to quit.");
				while (Console.ReadLine() != "q")
				{ }
				poller.Stop(true);
			}
		}

		private static void SubSocketOnReceiveReady(object sender, NetMQSocketEventArgs args)
		{
			NetMQMessage message = args.Socket.ReceiveMessage();

			Console.WriteLine("Message Received:");
			foreach (NetMQFrame frame in message)
			{
				Console.WriteLine("\t{0}", frame.ConvertToString());
			}
		}
	}
}
