using System;
using System.Threading.Tasks;
using NetMQ;

namespace ClientWithHelper
{
	public class MessageHelper : IDisposable
	{
		private readonly NetMQContext _context;
		private readonly NetMQSocket _subscribeSocket;
		private readonly Poller _poller;

		public MessageHelper(string address, string topic)
		{
			_context = NetMQContext.Create();
			_subscribeSocket = _context.CreateSubscriberSocket();
			_subscribeSocket.Connect(address);
			_subscribeSocket.ReceiveReady += SubscribeSocketOnReceiveReady;
			_subscribeSocket.Subscribe(topic);
			_poller = new Poller();
			_poller.AddSocket(_subscribeSocket);
			Task.Factory.StartNew(_poller.Start);
		}

		private void SubscribeSocketOnReceiveReady(object sender, NetMQSocketEventArgs args)
		{
			NetMQMessage message = args.Socket.ReceiveMessage();

			if (message.FrameCount >= 2)
			{
				MessageReceived.Invoke(this, new MessageEventArgs(message[0].ConvertToString(), message[1].ConvertToString()));
			}
		}

		public event EventHandler<MessageEventArgs> MessageReceived = delegate { };

		public void Dispose()
		{
			_poller.Stop();
			_subscribeSocket.Dispose();
			_context.Dispose();
		}
	}
}