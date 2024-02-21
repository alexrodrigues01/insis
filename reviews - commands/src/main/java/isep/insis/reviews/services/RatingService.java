package isep.insis.reviews.services;

import isep.insis.reviews.model.Rating;

import java.util.Optional;

public interface RatingService {

    Optional<Rating> findByRate(Double rate);
}
