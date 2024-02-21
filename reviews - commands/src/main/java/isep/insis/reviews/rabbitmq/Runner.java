package isep.insis.reviews.rabbitmq;

import com.fasterxml.jackson.databind.ObjectMapper;
import isep.insis.reviews.ReviewsApplication;
import isep.insis.reviews.model.EventDTO;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class Runner {

    private final RabbitTemplate rabbitTemplate;

    @Autowired
    private ObjectMapper objectMapper;

    public Runner( RabbitTemplate rabbitTemplate) {
        this.rabbitTemplate = rabbitTemplate;
    }

    public void sendMessage(EventDTO eventDTO) throws Exception {
        System.out.println("Sending message...");
        System.out.println(objectMapper.writeValueAsString(eventDTO));
        rabbitTemplate.convertAndSend(ReviewsApplication.fanoutExchangeName2, "", objectMapper.writeValueAsString(eventDTO));
    }

}