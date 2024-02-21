package isep.insis.products.rabbitmq;

import isep.insis.products.model.Event;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import isep.insis.products.model.ProductDTO;
import isep.insis.products.repositories.EventRepository;
import isep.insis.products.utils.EventDTO;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.LinkedHashMap;
import java.util.concurrent.CountDownLatch;

@Component
public class Receiver {

    @Autowired
    private ObjectMapper objectMapper;
    @Autowired
    private EventRepository repository;
    private CountDownLatch latch = new CountDownLatch(0);

    public void receiveMessage(String messageJson) throws JsonProcessingException {
        EventDTO message =  objectMapper.readValue(messageJson, EventDTO.class);
        repository.save(new Event(message.getTypeOfEvent(),message.getDomain(),message.getEntity()));
        latch.countDown();
    }

    public CountDownLatch getLatch() {
        return latch;
    }

}