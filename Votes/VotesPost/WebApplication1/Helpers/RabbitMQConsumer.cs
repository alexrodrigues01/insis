using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Votes.DTOs;
using Votes.Interfaces.Repositories;
using Votes.Model;

namespace Votes.Helpers
{
    public class RabbitMQConsumer : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private IServiceScopeFactory serviceScopeFactory;

        public RabbitMQConsumer(IServiceScopeFactory _serviceScopeFactory)
        {
            serviceScopeFactory = _serviceScopeFactory;
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {

            var RabbitMQServer = StaticConfigurationManager.AppSetting["RabbitMQ:RabbitURL"];
            var RabbitMQUserName = StaticConfigurationManager.AppSetting["RabbitMQ:Username"];
            var RabbitMQPassword = StaticConfigurationManager.AppSetting["RabbitMQ:Password"];


            var factory = new ConnectionFactory()
            { HostName = RabbitMQServer, UserName = RabbitMQUserName, Password = RabbitMQPassword };

            // create connection
            _connection = factory.CreateConnection();

            // create channel
            _channel = _connection.CreateModel();

            //Direct Exchange Details like name and type of exchange
            //_channel.ExchangeDeclare(StaticConfigurationManager.AppSetting["RabbitMqSettingsReview:ExchangeName"], StaticConfigurationManager.AppSetting["RabbitMqSettingsReview:ExchhangeType"]);

            //Declare Queue with Name and a few property related to Queue like durabality of msg, auto delete and many more
            _channel.QueueDeclare(queue: StaticConfigurationManager.AppSetting["RabbitMqSettingsReview:QueueName"],
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);


            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            _channel.QueueBind(queue: StaticConfigurationManager.AppSetting["RabbitMqSettingsReview:QueueName"], exchange: StaticConfigurationManager.AppSetting["RabbitMqSettingsReview:ExchangeName"], routingKey: "");

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // received message
                var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());

                // acknowledge the received message
                _channel.BasicAck(ea.DeliveryTag, false);

                //Deserilized Message
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                ReviewDTOQueue messageDTO = JsonConvert.DeserializeObject<ReviewDTOQueue>(message);
                var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
                var _repository = serviceProvider.GetService<IReviewRepository>();

                if (messageDTO.typeOfEvent == "CREATE" && messageDTO.domain == "Review")
                {
                    Review review = new Review(messageDTO.entity.idReview);
                    Task revieww = _repository.CreateReview(review);
                }

                //Object entityFinal;

                //MessageInformation messageDetails = null;
                //List<MessageInformation> listDetails = new List<MessageInformation>();

                //var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
                //var _service = serviceProvider.GetService<IMessageService>();

                //if (messageDTO.entity is JObject)
                //{
                //    entityFinal = ((JObject)messageDTO.entity).ToObject<VoteReviewDTO>();
                //    messageDetails = new MessageInformation(messageDTO.typeOfEvent.ToString(), messageDTO.domain, (VoteReviewDTO)entityFinal);
                //    _service.CreateMessage(messageDetails);
                //}
                //else if (messageDTO.entity is JArray)
                //{
                //    entityFinal = ((JArray)messageDTO.entity).ToObject<List<VoteReviewDTO>>();
                //    MessageInformation info = new MessageInformation(messageDTO.typeOfEvent.ToString(), messageDTO.domain, (List<VoteReviewDTO>)entityFinal);
                //    listDetails.Add(info);
                //    _service.CreateListMessage(listDetails);
                //}

            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(StaticConfigurationManager.AppSetting["RabbitMqSettingsReview:QueueName"], false, consumer);
            return Task.CompletedTask;
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
