package isep.insis.reviews.controllers;

import com.fasterxml.jackson.databind.ObjectMapper;
import isep.insis.reviews.model.*;
import isep.insis.reviews.rabbitmq.Runner;
import isep.insis.reviews.services.ReviewService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;


@Tag(name = "Review", description = "Endpoints for managing Review")
@RestController
public class ReviewController {

    @Autowired
    private ReviewService rService;

    @Autowired
    private Runner runner;

    @Operation(summary = "creates review")
    @PostMapping("/products/{sku}/reviews")
    public ResponseEntity<ReviewDTO> createReview(@PathVariable(value = "sku") final String sku, @RequestBody CreateReviewDTO createReviewDTO) throws Exception {

        final var review = rService.create(createReviewDTO, sku);

        if (review == null) {
            return ResponseEntity.badRequest().build();
        }

        runner.sendMessage(new EventDTO(TypeOfEvent.CREATE,"Review",ReviewMapper.toDtoQueue(review, createReviewDTO.getUserID(), sku)));
        //runner.sendMessage(new EventDTO(TypeOfEvent.CREATE,"Review","test"));
        return new ResponseEntity<ReviewDTO>(review, HttpStatus.CREATED);
    }

    @Operation(summary = "deletes review")
    @DeleteMapping("/reviews/{reviewID}")
    public ResponseEntity<Boolean> deleteReview(@PathVariable(value = "reviewID") final Long reviewID) throws Exception {

        Boolean rev = rService.deleteReview(reviewID);

        if (rev == null) return ResponseEntity.notFound().build();

        if (rev == false) return ResponseEntity.unprocessableEntity().build();

        runner.sendMessage(new EventDTO(TypeOfEvent.DELETE,"Review", ReviewMapper.toDtoQueue(new ReviewDTO(reviewID,null,null,null,null,null),null, null)));

        return ResponseEntity.ok().body(rev);
    }

    @Operation(summary = "Accept or reject review")
    @PutMapping("/reviews/acceptreject/{reviewID}")
    public ResponseEntity<ReviewDTO> putAcceptRejectReview(@PathVariable(value = "reviewID") final Long reviewID, @RequestBody String approved) throws Exception {

        try {
            ReviewDTO rev = rService.moderateReview(reviewID, approved);
            runner.sendMessage(new EventDTO(TypeOfEvent.UPDATE,"Review",new AcceptRejectReviewDTO(reviewID, approved)));
            return ResponseEntity.ok().body(rev);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.badRequest().build();
        } catch (ResourceNotFoundException e) {
            return ResponseEntity.notFound().build();
        }
    }
}
