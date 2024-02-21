package isep.insis.reviews.services;

import isep.insis.reviews.model.*;

public interface ReviewService {

    ReviewDTO create(CreateReviewDTO createReviewDTO, String sku);

    Boolean deleteReview(Long reviewId);


    ReviewDTO moderateReview(Long reviewID, String approved);

}
