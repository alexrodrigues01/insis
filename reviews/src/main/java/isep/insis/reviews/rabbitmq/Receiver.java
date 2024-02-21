package isep.insis.reviews.rabbitmq;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import isep.insis.reviews.model.*;
import isep.insis.reviews.services.ProductService;
import isep.insis.reviews.services.ReviewService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.LinkedHashMap;
import java.util.concurrent.CountDownLatch;

@Component
public class Receiver {
    private CountDownLatch latch = new CountDownLatch(1);

    @Autowired
    private ProductService service;

    @Autowired
    ReviewService reviewService;

    @Autowired
    private ObjectMapper objectMapper;

    public void receiveMessage(String messageJson) throws JsonProcessingException {
        EventDTO message =  objectMapper.readValue(messageJson, EventDTO.class);
        System.out.println("Received <" + message.toString() + ">");
        LinkedHashMap<String, Object> entityMap = (LinkedHashMap<String, Object>) message.getEntity();
        try {
            if (message.getDomain().equals("Review")) {
                switch (message.getTypeOfEvent()) {
                    case CREATE -> {
                        ReviewDTOQueue reviewDTO = objectMapper.convertValue(entityMap, ReviewDTOQueue.class);
                        reviewService.create(reviewDTO);
                    }
                    case DELETE -> {
                        ReviewDTOQueue reviewDTO = objectMapper.convertValue(entityMap, ReviewDTOQueue.class);
                        reviewService.delete(reviewDTO.getIdReview());
                    }
                    case UPDATE -> {
                        AcceptRejectReviewDTO acceptRejectReviewDTO = objectMapper.convertValue(entityMap, AcceptRejectReviewDTO.class);
                        reviewService.update(acceptRejectReviewDTO);
                    }
                }
            }else{
                ProductDTO productDTO = objectMapper.convertValue(entityMap, ProductDTO.class);
                switch (message.getTypeOfEvent()) {
                    case CREATE -> {
                        service.create(new Product(productDTO.getSku()));
                    }
                    case DELETE -> {
                        service.deleteBySku(productDTO.getSku());
                    }
                }
            }
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
        latch.countDown();
    }

    public CountDownLatch getLatch() {
        return latch;
    }
}
