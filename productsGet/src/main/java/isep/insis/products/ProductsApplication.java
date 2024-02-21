package isep.insis.products;


import isep.insis.products.property.FileStorageProperties;
import isep.insis.products.rabbitmq.Receiver;
import org.springframework.amqp.core.*;
import org.springframework.amqp.rabbit.connection.ConnectionFactory;
import org.springframework.amqp.rabbit.listener.SimpleMessageListenerContainer;
import org.springframework.amqp.rabbit.listener.adapter.MessageListenerAdapter;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.context.annotation.Bean;


@SpringBootApplication
@EnableConfigurationProperties({
		FileStorageProperties.class
})
public class ProductsApplication {

	static final String fanoutExchangeName = "my-fanout-exchange-2";

	static final String queueName = "queue-get-products";

	static final String queueBootstrap= "queue-bootstrap-prodcuts";

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
	@Bean
	Binding binding(Queue queueBootstrap, FanoutExchange exchange) {
		return BindingBuilder.bind(queueBootstrap).to(exchange);
	}
	public static void main(String[] args) {
		SpringApplication.run(ProductsApplication.class, args);
	}



}
