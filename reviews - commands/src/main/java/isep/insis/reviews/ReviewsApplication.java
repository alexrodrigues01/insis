package isep.insis.reviews;


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

	public static final String fanoutExchangeName = "my-fanout-exchange";

	public static final String fanoutExchangeName2 = "my-fanout-exchange2";

	static final String queue4Name = "queue-products-reviews";
	static final String queue1Name = "queue-bootstrap-reviews";
	static final String queue2Name = "queue-reviews";
	static final String queue3Name = "queue-votes";

	@Bean
	Queue queue1() {
		return new Queue(queue1Name, false);
	}

	@Bean
	Queue queue4() {
		return new Queue(queue4Name, false);
	}

	@Bean
	Queue queue2() {
		return new Queue(queue2Name, false);
	}

	@Bean
	Queue queue3() {
		return new Queue(queue3Name, false);
	}
	@Bean
	FanoutExchange fanoutExchange() {
		return new FanoutExchange(fanoutExchangeName);
	}
	@Bean
	FanoutExchange fanoutExchange2() {
		return new FanoutExchange(fanoutExchangeName2);
	}
	@Bean
	Binding binding4(Queue queue4, FanoutExchange fanoutExchange) {
		return BindingBuilder.bind(queue4).to(fanoutExchange);
	}
	@Bean
	Binding binding1(Queue queue1, FanoutExchange fanoutExchange2) {
		return BindingBuilder.bind(queue1).to(fanoutExchange2);
	}

	@Bean
	Binding binding2(Queue queue2, FanoutExchange fanoutExchange2) {
		return BindingBuilder.bind(queue2).to(fanoutExchange2);
	}

	@Bean
	Binding binding3(Queue queue3, FanoutExchange fanoutExchange2) {
		return BindingBuilder.bind(queue3).to(fanoutExchange2);
	}

	@Bean
	MessageListenerAdapter listenerAdapter(Receiver receiver) {
		MessageListenerAdapter messageListenerAdapter = new MessageListenerAdapter(receiver, "receiveMessage");
		return new MessageListenerAdapter(receiver, "receiveMessage");
	}

	@Bean
	SimpleMessageListenerContainer container(ConnectionFactory connectionFactory,
											 MessageListenerAdapter listenerAdapter) {
		SimpleMessageListenerContainer container = new SimpleMessageListenerContainer();
		container.setConnectionFactory(connectionFactory);
		container.setQueues(queue4());
		container.setMessageListener(listenerAdapter);
		return container;
	}

	public static void main(String[] args) {
		SpringApplication.run(ReviewsApplication.class, args);
	}



}
