// See https://aka.ms/new-console-template for more information
using Azure.Storage.Queues;
using QueueMessageConsumer;

Console.WriteLine("Hello, World!");

var connectionString = "DefaultEndpointsProtocol=https;AccountName=projectname1sane;AccountKey=Z7BDYxJqQgFKvJPpw7NcKPpURQkOraIPpO74njujqaWgVKBrs/qP4eHgp9r4taV1l/xQk00I4pFa+AStyjfaMw==;EndpointSuffix=core.windows.net";

var queueName = "returns";

QueueClient queueClient = new QueueClient(connectionString, queueName);

while (true)
{
    var message = queueClient.ReceiveMessage();
    if (message.Value != null)
    {
        var dto = message.Value.Body.ToObjectFromJson<ReturnDto>();
        Process(dto);
        await queueClient.DeleteMessageAsync(message.Value.MessageId, message.Value.PopReceipt);
    }

    await Task.Delay(1000);
}

void Process(ReturnDto dto)
{
    Console.WriteLine($"Processing return with id: {dto.Id}, " +
        $"for user : {dto.User}" +
        $"from address: {dto.Address}");
}