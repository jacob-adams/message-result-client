using System;
using System.Data;

namespace ClientWithHelper
{
	public class ClientWithHelperProgram
	{
		static void Main(string[] args)
		{
			using (MessageHelper helper = new MessageHelper(address: "tcp://127.0.0.1:5002", topic: ""))
			{
				Console.WriteLine("Waiting for messages. Type \"q\" to quit.");
				helper.MessageReceived += (sender, eventArgs) =>
				{
					Console.WriteLine("Topic: {0}", eventArgs.Topic);
					Console.WriteLine("RootID: {0}", eventArgs.RootID);
					int i = 0;
					//display the information in the table
					foreach (DataRow row in eventArgs.Table.Rows)
					{
						Console.WriteLine("Row {0}", ++i);
						foreach (DataColumn column in eventArgs.Table.Columns)
						{
							Console.WriteLine("\t{0}: {1}", column.ColumnName, row[column]);
						}
					}
				};

				//wait until the user types "q" to quit
				while (Console.ReadLine() != "q")
				{ }
			}
		}
	}
}
