using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using VotesGet.DTOs;
using VotesGet.Helpers;
using VotesGet.Interfaces.Repository;
using VotesGet.Interfaces.Services;
using VotesGet.Model;
using static VotesGet.DTOs.MessageInformationDTO;

namespace VotesGet.Services;

public class VoteService : IVoteService
{
    private readonly IVoteRepository _repository;

    private readonly IMapper _mapper;

    public VoteService(IVoteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;   
    }

    public async Task<List<VoteReviewDTO>> GetVoteByReview(long reviewID)
    {
        if (reviewID == 0)
        {
            throw new Exception("Review not found.");
        }

        List<VoteReviewDTO> result = new List<VoteReviewDTO>();

        result = await _repository.GetVoteByReview(reviewID);

        return result;
    }

    public bool SendMessageInformation(List<VoteReviewDTO> listVoteDTO)
    {

        var RabbitMQServer = StaticConfigurationManager.AppSetting["RabbitMQ:RabbitURL"];
        var RabbitMQUserName = StaticConfigurationManager.AppSetting["RabbitMQ:Username"];
        var RabbitMQPassword = StaticConfigurationManager.AppSetting["RabbitMQ:Password"];

        try
        {
            var factory = new ConnectionFactory()
            { HostName = RabbitMQServer, UserName = RabbitMQUserName, Password = RabbitMQPassword };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Direct Exchange Details like name and type of exchange
                channel.ExchangeDeclare(StaticConfigurationManager.AppSetting["RabbitMqSettingsMessage:ExchangeName"], StaticConfigurationManager.AppSetting["RabbitMqSettingsMessage:ExchhangeType"]);

                //Declare Queue with Name and a few property related to Queue like durabality of msg, auto delete and many more
                channel.QueueDeclare(queue: StaticConfigurationManager.AppSetting["RabbitMqSettingsMessage:QueueName"],
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //Bind Queue with Exhange and routing details
                channel.QueueBind(queue: StaticConfigurationManager.AppSetting["RabbitMqSettingsMessage:QueueName"], exchange: StaticConfigurationManager.AppSetting["RabbitMqSettingsMessage:ExchangeName"], routingKey: string.Empty);

                MessageInformationDTO enviar = new MessageInformationDTO(TypeOfEvent.GET, "Vote", listVoteDTO);

                string voteMessage = JsonConvert.SerializeObject(enviar);

                var body = Encoding.UTF8.GetBytes(voteMessage);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                //publish msg
                channel.BasicPublish(exchange: StaticConfigurationManager.AppSetting["RabbitMqSettingsMessage:ExchangeName"],
                                     routingKey: string.Empty,
                                     basicProperties: properties,
                                     body: body);
                return true;
            }
        }

        catch (Exception)
        {
        }
        return false;
    }

}