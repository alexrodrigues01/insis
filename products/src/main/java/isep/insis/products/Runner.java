package isep.insis.products;



import com.fasterxml.jackson.core.JsonProcessingException;
import isep.insis.products.utils.EventDTO;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import com.fasterxml.jackson.databind.ObjectMapper;

@Component
public class Runner  {

    private final RabbitTemplate rabbitTemplate;

    @Autowired
    private ObjectMapper objectMapper;

    public Runner( RabbitTemplate rabbitTemplate) {
        this.rabbitTemplate = rabbitTemplate;
    }


    public void sendMessage(EventDTO eventDTO) throws JsonProcessingException {
        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(ProductsApplication.fanoutExchangeName,"",objectMapper.writeValueAsString(eventDTO));
    }

}
