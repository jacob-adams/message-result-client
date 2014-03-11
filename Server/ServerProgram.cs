using System;
using System.Collections.Generic;
using NetMQ;

namespace Server
{
	class ServerProgram
	{
		static void Main(string[] args)
		{
			using (NetMQContext context = NetMQContext.Create())
			using (NetMQSocket pubSocket = context.CreatePublisherSocket())
			{
				pubSocket.Bind("tcp://*:5489");

				Console.WriteLine("Type a message to be sent to each connected client.  Type 'q' to quit.");
				string line;
				while ((line = Console.ReadLine()) != "q")
				{
					NetMQMessage message = new NetMQMessage(new List<NetMQFrame>{new NetMQFrame("Topic"), new NetMQFrame(line)});
					pubSocket.SendMessage(message);
				}
			}
		}
	}
}
