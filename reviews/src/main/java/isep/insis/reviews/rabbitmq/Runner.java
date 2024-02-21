package isep.insis.reviews.rabbitmq;

import com.fasterxml.jackson.databind.ObjectMapper;
import isep.insis.reviews.ReviewsApplication;
import isep.insis.reviews.model.EventDTO;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.concurrent.TimeUnit;

@Component
public class Runner {

    private final RabbitTemplate rabbitTemplate;
    private final Receiver receiver;

    @Autowired
    private ObjectMapper objectMapper;

    public Runner(Receiver receiver, RabbitTemplate rabbitTemplate) {
        this.receiver = receiver;
        this.rabbitTemplate = rabbitTemplate;
    }


    public void sendMessage(EventDTO eventDTO) throws Exception {
        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(ReviewsApplication.fanoutExchangeName, "", objectMapper.writeValueAsString(eventDTO));
        receiver.getLatch().await(10000, TimeUnit.MILLISECONDS);
    }
}