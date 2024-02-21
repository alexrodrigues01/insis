using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Votes.DTOs;
using Votes.Helpers;
using Votes.Interfaces.Repositories;
using VotesApp.DTOs;
using VotesApp.Interfaces.Repository;
using VotesApp.Interfaces.Services;
using VotesApp.Model;
using static Votes.DTOs.MessageInformationDTO;

namespace VotesApp.Services;

public class VoteService : IVoteService
{
    private readonly IVoteRepository _repository;

    private readonly IReviewRepository _reviewRepository;

    private readonly IMapper _mapper;

    public VoteService(IVoteRepository repository, IMapper mapper, IReviewRepository reviewRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _reviewRepository = reviewRepository;
    }
    
    public async Task<VoteReviewDTO> CreateVote(VoteReviewDTO voteDTO)
    {
        if (voteDTO.reviewId == 0)
        {
            throw new Exception("Review not found.");
        }

        bool checkReview = true;

        checkReview = _reviewRepository.FindReview(voteDTO.reviewId);
        if (checkReview){
            Vote vote = new Vote(voteDTO.vote, voteDTO.userId, voteDTO.reviewId);
            if (voteDTO.vote.Equals("upVote", StringComparison.OrdinalIgnoreCase))
            {
                if (!await _repository.FindVote(voteDTO))
                {
                    throw new Exception("Vote already made.");
                }
                else
                {
                    await _repository.CreateVote(vote);
                }
            }
            else if (voteDTO.vote.Equals("downVote", StringComparison.OrdinalIgnoreCase))
            {
                if (!await _repository.FindVote(voteDTO))
                {
                    throw new Exception("Vote already made.");
                }
                else
                {
                    await _repository.CreateVote(vote);
                }
            }
            else
            {
                throw new Exception("Vote Invalid");
            }
        }
        else
        {
            throw new Exception("Review does not exist");
        }

        return voteDTO;
    }

    public bool SendVote(VoteReviewDTO voteDTO)
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
                channel.ExchangeDeclare(StaticConfigurationManager.AppSetting["RabbitMqSettings:ExchangeName"], StaticConfigurationManager.AppSetting["RabbitMqSettings:ExchhangeType"]);

                //Declare Queue with Name and a few property related to Queue like durabality of msg, auto delete and many more
                channel.QueueDeclare(queue: StaticConfigurationManager.AppSetting["RabbitMqSettings:QueueName"],
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //Bind Queue with Exhange and routing details
                channel.QueueBind(queue: StaticConfigurationManager.AppSetting["RabbitMqSettings:QueueName"], exchange: StaticConfigurationManager.AppSetting["RabbitMqSettings:ExchangeName"], routingKey: string.Empty);

                //Seriliaze object using Newtonsoft library
                string voteDetail = JsonConvert.SerializeObject(voteDTO);
                var body = Encoding.UTF8.GetBytes(voteDetail);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                //publish msg
                channel.BasicPublish(exchange: StaticConfigurationManager.AppSetting["RabbitMqSettings:ExchangeName"],
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

    public bool SendMessageInformation(VoteReviewDTO voteDTO)
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

                //Seriliaze object using Newtonsoft library
                MessageInformationDTO messageDTO = new MessageInformationDTO(TypeOfEvent.CREATE, "Vote",voteDTO);

                string voteMessage = JsonConvert.SerializeObject(messageDTO);

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