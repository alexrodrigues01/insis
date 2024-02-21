package isep.insis.reviews.services;

import isep.insis.reviews.controllers.ResourceNotFoundException;
import isep.insis.reviews.model.*;
import isep.insis.reviews.repositories.ProductRepository;
import isep.insis.reviews.repositories.ReviewRepository;
import lombok.AllArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDate;
import java.util.List;
import java.util.Optional;

@Service
public class ReviewServiceImpl implements ReviewService {

    @Autowired
    ReviewRepository repository;

    @Autowired
    RatingService ratingService;

    @Override
    public Iterable<Review> getAll() {
        return null;
    }

    //
    @Autowired
    ProductRepository pRepository;
//
//    @Autowired
//    UserRepository uRepository;
//
//    @Autowired
//    UserService userService;
//
//    @Autowired
//    RatingService ratingService;
//
//    @Autowired
//    RestService restService;

    //    @Override
//    public Iterable<Review> getAll() {
//        return repository.findAll();
//    }
    @Override
    public List<ReviewDTO> getReviewsOfProduct(String sku, String status) {

        Optional<Product> product = pRepository.findBySku(sku);
        if (product.isEmpty()) return null;

        Optional<List<Review>> r = repository.findByProductIdStatus(product.get(), status);

        if (r.isEmpty()) return null;

        return ReviewMapper.toDtoList(r.get());
    }

    @Override
    public Double getWeightedAverage(Product product){

        Optional<List<Review>> r = repository.findByProductId(product);

        if (r.isEmpty()) return 0.0;

        double sum = 0;

        for (Review rev: r.get()) {
            Rating rate = rev.getRating();

            if (rate != null){
                sum += rate.getRate();
            }
        }

        return sum/r.get().size();
    }

    @Override
    public void create(ReviewDTOQueue reviewDTO) {

        final Optional<Product> product = pRepository.findBySku(reviewDTO.getSku());

        if(product.isEmpty())
            return;

        Rating rating = null;
        Optional<Rating> r = ratingService.findByRate(reviewDTO.getRating());
        if(r.isPresent()) {
            rating = r.get();
        }

        LocalDate date = LocalDate.now();

        //Review review = new Review(createReviewDTO.getReviewText(), date, product.get(), funfact, rating, user.get());
        Review review = new Review(reviewDTO.getIdReview(), reviewDTO.getReviewText(), date, product.get(), reviewDTO.getFunFact(), rating, reviewDTO.getUserID());

        repository.save(review);
    }

    @Override
    public List<ReviewDTO> findPendingReview(){

        Optional<List<Review>> r = repository.findPendingReviews();

        if(r.isEmpty()){
            return null;
        }

        return ReviewMapper.toDtoList(r.get());
    }
//
//    @Override
//    public ReviewDTO moderateReview(Long reviewID, String approved) throws ResourceNotFoundException, IllegalArgumentException {
//
//        Optional<Review> r = repository.findById(reviewID);
//
//        if(r.isEmpty()){
//            throw new ResourceNotFoundException("Review not found");
//        }
//
//        Boolean ap = r.get().setApprovalStatus(approved);
//
//        if(!ap) {
//            throw new IllegalArgumentException("Invalid status value");
//        }
//
//        Review review = repository.save(r.get());
//
//        return ReviewMapper.toDto(review);
//    }
//
//
    @Override
    public List<ReviewDTO> findReviewsByUser(Long userID) {

        Optional<List<Review>> r = repository.findByUserId(userID);

        if (r.isEmpty()) return null;

        return ReviewMapper.toDtoList(r.get());
       // return ReviewMapper.toDtoList(null);
    }

    @Override
    public void delete(Long reviewID) {


        Optional<Review> rev = repository.findById(reviewID);

        if (rev.isEmpty()){
            return;
        }
        Review r = rev.get();

        repository.delete(r);
    }

    @Override
    public void update(AcceptRejectReviewDTO acceptRejectReviewDTO) {

        Optional<Review> r = repository.findById(acceptRejectReviewDTO.getReviewId());

        if(r.isEmpty()){
            throw new ResourceNotFoundException("Review not found");
        }

        Boolean ap = r.get().setApprovalStatus(acceptRejectReviewDTO.getApproved());

        if(!ap) {
            throw new IllegalArgumentException("Invalid status value");
        }

        Review review = repository.save(r.get());
    }
}