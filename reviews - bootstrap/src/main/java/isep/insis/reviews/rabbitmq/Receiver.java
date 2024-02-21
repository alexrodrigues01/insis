package isep.insis.reviews.rabbitmq;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import isep.insis.reviews.model.Event;
import isep.insis.reviews.repositories.EventRepository;
import isep.insis.reviews.model.EventDTO;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.LinkedHashMap;
import java.util.concurrent.CountDownLatch;

@Component
public class Receiver {
    private CountDownLatch latch = new CountDownLatch(1);

    @Autowired
    private EventRepository repository;

    @Autowired
    private ObjectMapper objectMapper;

    public void receiveMessage(String messageJson) throws JsonProcessingException {
        EventDTO message =  objectMapper.readValue(messageJson, EventDTO.class);
        System.out.println("Received <" + message.toString() + ">");
        repository.save(new Event(message.getTypeOfEvent(),message.getDomain(),message.getEntity()));
        latch.countDown();
    }

    public CountDownLatch getLatch() {
        return latch;
    }
}
