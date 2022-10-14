using Azure.Messaging.ServiceBus;

Console.WriteLine("Service Bus Consumer");

var connectionString = "Endpoint=sb://project1sbne.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=9MPCij7AP/uy2BPNCv613tnnJgRT3AbYGKwC7bVYHzc=";

var client = new ServiceBusClient(connectionString);

var processor = client.CreateProcessor("user-registered", "subscription2");

processor.ProcessMessageAsync += MessageHandler;
processor.ProcessErrorAsync += ErrorHandler;

Console.WriteLine("Start processing");
await processor.StartProcessingAsync();

Console.ReadKey();

Task ErrorHandler(ProcessErrorEventArgs arg)
{
    Console.WriteLine(arg.ErrorSource);
    Console.WriteLine(arg.Exception.ToString());
    return Task.CompletedTask;
}

Task MessageHandler(ProcessMessageEventArgs arg)
{
    string body = arg.Message.Body.ToString();
    Console.WriteLine(body);

    return Task.CompletedTask;
}