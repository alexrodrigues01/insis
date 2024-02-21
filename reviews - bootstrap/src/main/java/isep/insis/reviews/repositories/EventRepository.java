package isep.insis.reviews.repositories;

import isep.insis.reviews.model.Event;
import org.springframework.data.mongodb.repository.MongoRepository;

public interface EventRepository extends MongoRepository<Event,String> {
}
