using System;
using System.Data;
using Newtonsoft.Json;

namespace ClientWithHelper
{
	public class MessageEventArgs : EventArgs
	{
		private readonly MessageObject _data;

		public MessageEventArgs(string topic, string json)
		{
			Topic = topic;
			if (!String.IsNullOrWhiteSpace(json))
			{
				_data = JsonConvert.DeserializeObject<MessageObject>(json);
			}
			else
			{
				_data = new MessageObject();
			}
		}

		public string Topic { get; private set; }

		public string RootID { get { return _data.RootID; } }

		public DataTable Table { get { return _data.Table; } }

		private class MessageObject
		{
			public string RootID { get; set; }
			public DataTable Table { get; set; }
		}
	}
}