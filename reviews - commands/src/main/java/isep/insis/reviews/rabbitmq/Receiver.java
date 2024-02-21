package isep.insis.reviews.rabbitmq;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import isep.insis.reviews.model.EventDTO;
import isep.insis.reviews.model.Product;
import isep.insis.reviews.model.ProductDTO;
import isep.insis.reviews.model.TypeOfEvent;
import isep.insis.reviews.services.ProductService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.LinkedHashMap;
import java.util.concurrent.CountDownLatch;

@Component
public class Receiver {

    @Autowired
    private ProductService service;

    @Autowired
    private ObjectMapper objectMapper;

    @Autowired
    private Runner runner;
    private CountDownLatch latch = new CountDownLatch(0);

    public void receiveMessage(String messageJson) throws JsonProcessingException {
        try {
            EventDTO message = objectMapper.readValue(messageJson, EventDTO.class);
            System.out.println("Received <" + message.toString() + ">");
            LinkedHashMap<String, Object> entityMap = (LinkedHashMap<String, Object>) message.getEntity();
            ProductDTO productDTO = objectMapper.convertValue(entityMap, ProductDTO.class);
            switch (message.getTypeOfEvent()) {
                case CREATE -> {
                    service.create(new Product(productDTO.getSku()));
                    runner.sendMessage(new EventDTO(TypeOfEvent.CREATE,"Product", new ProductDTO( productDTO.getSku())));
                }
                case DELETE -> {
                    service.deleteBySku(productDTO.getSku());
                    runner.sendMessage(new EventDTO(TypeOfEvent.DELETE,"Product", new ProductDTO( productDTO.getSku())));
                }
            }
        }catch (Exception e){
            System.out.println(e);
        }
        latch.countDown();
    }


    public CountDownLatch getLatch() {
        return latch;
    }

}