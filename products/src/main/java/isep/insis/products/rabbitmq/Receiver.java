//package isep.insis.products.rabbitmq;
//
//import isep.insis.products.model.Product;
//import isep.insis.products.model.ProductDTO;
//import isep.insis.products.services.ProductService;
//import isep.insis.products.utils.EventDTO;
//import org.springframework.beans.factory.annotation.Autowired;
//import org.springframework.stereotype.Component;
//
//import java.util.concurrent.CountDownLatch;
//
//@Component
//public class Receiver {
//
//    @Autowired
//    private ProductService service;
//    private CountDownLatch latch = new CountDownLatch(1);
//
//    public void receiveMessage(EventDTO message) {
////        System.out.println("Received boda«<" + message.getEntity().toString() + ">");
////        ProductDTO productDTO= ((ProductDTO) message.getEntity());
////        service.create(new Product(productDTO.getSku(),productDTO.getDesignation(),productDTO.getDesignation()));
////        latch.countDown();
//    }
//
//    public CountDownLatch getLatch() {
//        return latch;
//    }
//
//}