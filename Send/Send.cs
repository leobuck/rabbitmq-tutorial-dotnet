using System;
using RabbitMQ.Client;
using System.Text;


class Send
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "10.0.0.64", Port = 5672, UserName = "timais", Password = "brc943N", VirtualHost = "timais" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "rpc_camera_queue", durable: false, exclusive: false, autoDelete: true, arguments: null);

            string message = "rtsp://admin:admin@inside.timais.com:554/cam/realmonitor?channel=2&subtype=0";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "gflow.topic", routingKey: "gflow.tirar.foto.*", basicProperties: null, body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}

