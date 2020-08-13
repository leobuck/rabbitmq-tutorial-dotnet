using System;
using RabbitMQ.Client;
using System.Text;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            string hostName = "localhost";
            string queueName = "Message-Queue";
            //Cria a conexão com o RabbitMq
            var factory = new ConnectionFactory()
            {
                HostName = hostName,
            };
            //Cria a conexão
            IConnection connection = factory.CreateConnection();
            //cria a canal de comunicação com a rabbit mq
            IModel channel = connection.CreateModel();
            //Cria a fila caso não exista
            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            for (int i = 1; i <= 500; i++)
            {
                String mensagem = $"Enviando a mensagem {i}";
                byte[] body = Encoding.Default.GetBytes(mensagem);
                //Seta a mensagem como persistente
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                //Envia a mensagem para fila
                channel.BasicPublish(exchange: String.Empty, routingKey: queueName, basicProperties: properties, body:body);
            }
            Console.WriteLine("Mensagem enviadas para fila");
            Console.Read();            
        }
    }
}
