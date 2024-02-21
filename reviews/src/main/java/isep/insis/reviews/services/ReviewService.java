package isep.insis.reviews.services;

import isep.insis.reviews.model.*;
import org.springframework.stereotype.Service;

import java.util.List;
public interface ReviewService {

    Iterable<Review> getAll();

    List<ReviewDTO> getReviewsOfProduct(String sku, String status);

    List<ReviewDTO> findPendingReview();

    List<ReviewDTO> findReviewsByUser(Long userID);

    Double getWeightedAverage(Product product);

    void create(ReviewDTOQueue reviewDTO);

    void delete(Long reviewID);

    void update(AcceptRejectReviewDTO acceptRejectReviewDTO);
}
