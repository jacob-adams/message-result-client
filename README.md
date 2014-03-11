# MessageResult Business Rule Client

### Description:
This Visual Studio solution contains two client applications that show how to interact with the JustWare MessageResult business rule.  There is a server project as well that can be used to test the clients if you would like to test it without the business rule.

The messaging that is used is based on a Nuget library called NetMQ. [NetMQ](https://github.com/zeromq/netmq) is a C# port of the popular [ØMQ](http://zeromq.org) library.  The business rule and client application use a publish-subscribe model to pass messages.  When the MessageResult business rule runs, it publishes a message with a specific topic.  The client application subscribes to the same topic so that it can receive messages sent with that topic.  The clients in this solution use an empty string for the topic so that they receive all messages published to the endpoint.

### Project: Client

The first client uses the NetMQ library directly.  It connects to the server endpoint (configured in the Maintenance Console) and subscribes to an empty string so that it receives all messages published to that endpoint.  When a message is received, it prints the raw message to the console.  Messages received from the business rule contain two frames (parts).  The first frame contains the topic, and the second frame is the JSon-serialized DataTable of the working data of the business rule.

### Project: ClientWithHelper

The second client is similar to the first client, except it abstracts all the socket connections to a helper class.  The main program just uses the helper class.  The Helper class has an event that it can subscribe to for when a message is received.  The argument parameter of the event contains typed properties for the topic, root id, and DataTable for the message that was received.

### Project: Server

This server can be used to send test messages to the clients without the need of using JustWare and the MessageResult business rule.  It binds to a port specified in the code and then will publish messages to that endpoint.  After the server starts, type a line of text on the command line and then press enter.  The message will be sent to all attached clients.

### Testing

To test the code, start the server and then start one or more clients (use the Client project since the ClientWithHelper is expecting messages in a specific format).  Type a line of text on the server command line.  The text should be sent to all connected clients.