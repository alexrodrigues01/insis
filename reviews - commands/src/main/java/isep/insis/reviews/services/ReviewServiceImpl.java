package isep.insis.reviews.services;

import isep.insis.reviews.controllers.ResourceNotFoundException;
import isep.insis.reviews.model.*;
import isep.insis.reviews.repositories.ProductRepository;
import isep.insis.reviews.repositories.ReviewRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDate;
import java.util.Optional;

@Service
public class ReviewServiceImpl implements ReviewService {

    @Autowired
    ReviewRepository repository;

    @Autowired
    ProductRepository pRepository;

    @Autowired
    RatingService ratingService;

    @Autowired
    RestService restService;

    @Override
    public ReviewDTO create(final CreateReviewDTO createReviewDTO, String sku) {

        final Optional<Product> product = pRepository.findBySku(sku);

        if(product.isEmpty()) return null;

//        final var user = userService.getUserId(createReviewDTO.getUserID());
//
//        if(user.isEmpty()) return null;

        Rating rating = null;
        Optional<Rating> r = ratingService.findByRate(createReviewDTO.getRating());
        if(r.isPresent()) {
            rating = r.get();
        }

        LocalDate date = LocalDate.now();

        String funfact = restService.getFunFact(date);

        if (funfact == null) return null;

        //Review review = new Review(createReviewDTO.getReviewText(), date, product.get(), funfact, rating, user.get());
        Review review = new Review(createReviewDTO.getReviewText(), date, product.get(), funfact, rating, createReviewDTO.getUserID());

        review = repository.save(review);

        if (review == null) return null;

        return ReviewMapper.toDto(review);
    }

    @Override
    public Boolean deleteReview(Long reviewId)  {

        Optional<Review> rev = repository.findById(reviewId);

        if (rev.isEmpty()){
            return null;
        }
        Review r = rev.get();

        repository.delete(r);

        return true;
    }
    @Override
    public ReviewDTO moderateReview(Long reviewID, String approved) throws ResourceNotFoundException, IllegalArgumentException {

        Optional<Review> r = repository.findById(reviewID);

        if(r.isEmpty()){
            throw new ResourceNotFoundException("Review not found");
        }

        Boolean ap = r.get().setApprovalStatus(approved);

        if(!ap) {
            throw new IllegalArgumentException("Invalid status value");
        }

        Review review = repository.save(r.get());

        return ReviewMapper.toDto(review);
    }
}