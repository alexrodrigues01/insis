package isep.insis.reviews;

import isep.insis.reviews.controllers.ReviewController;
import isep.insis.reviews.rabbitmq.Receiver;
import org.springframework.amqp.core.*;
import org.springframework.amqp.rabbit.connection.ConnectionFactory;
import org.springframework.amqp.rabbit.listener.SimpleMessageListenerContainer;
import org.springframework.amqp.rabbit.listener.adapter.MessageListenerAdapter;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class ReviewsApplication {

	public static final String fanoutExchangeName = "my-fanout-exchange3";

	static final String queueName = "queue-reviews";

	static final String queueBootstrap= "queue-bootstrap-reviews";

	@Bean
	Queue queue() {
		return new Queue(queueName, false);
	}
	@Bean
	Queue queueBootstrap() {
		return new Queue(queueBootstrap, false);
	}


	@Bean
	FanoutExchange fanoutExchange() {
		return new FanoutExchange(fanoutExchangeName);
	}

	@Bean
	Binding binding(Queue queueBootstrap, FanoutExchange exchange) {
		return BindingBuilder.bind(queueBootstrap).to(exchange);
	}
	@Bean
	SimpleMessageListenerContainer container(ConnectionFactory connectionFactory,
											 MessageListenerAdapter listenerAdapter) {
		SimpleMessageListenerContainer container = new SimpleMessageListenerContainer();
		container.setConnectionFactory(connectionFactory);
		container.setQueueNames(queueName);
		container.setMessageListener(listenerAdapter);
		return container;
	}


	@Bean
	MessageListenerAdapter listenerAdapter(Receiver receiver) {
		return new MessageListenerAdapter(receiver, "receiveMessage");
	}


	public static void main(String[] args) {
		SpringApplication.run(ReviewsApplication.class, args);
	}

}
