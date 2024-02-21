package isep.insis.reviews.controllers;

import isep.insis.reviews.model.EventDTO;
import isep.insis.reviews.model.ReviewDTO;
import isep.insis.reviews.model.ReviewMapper;
import isep.insis.reviews.model.TypeOfEvent;
import isep.insis.reviews.rabbitmq.Runner;
import isep.insis.reviews.services.ReviewService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;


@Tag(name = "Review", description = "Endpoints for managing Review")
@RestController
public class ReviewController {

    @Autowired
    private Runner runner;
    @Autowired
    private ReviewService rService;

    @Operation(summary = "finds a product through its sku and shows its review by status")
    @GetMapping("/products/{sku}/reviews/{status}")
    public ResponseEntity<List<ReviewDTO>> findById(@PathVariable(value = "sku") final String sku, @PathVariable(value = "status") final String status) throws Exception {

        final var review = rService.getReviewsOfProduct(sku, status);
        runner.sendMessage(new EventDTO(TypeOfEvent.GET,"Review", review));

        return ResponseEntity.ok().body( review );
    }

    @Operation(summary = "gets review by user")
    @GetMapping("/reviews/{userID}")
    public ResponseEntity<List<ReviewDTO>> findReviewByUser(@PathVariable(value = "userID") final Long userID) throws Exception {

        final var review = rService.findReviewsByUser(userID);
        runner.sendMessage(new EventDTO(TypeOfEvent.GET,"Review", review));

        return ResponseEntity.ok().body(review);
    }

    @Operation(summary = "gets pedding reviews")
    @GetMapping("/reviews/pending")
    public ResponseEntity<List<ReviewDTO>> getPendingReview() throws Exception {

        List<ReviewDTO> r = rService.findPendingReview();
        runner.sendMessage(new EventDTO(TypeOfEvent.GET,"Review", r));

        return ResponseEntity.ok().body(r);
    }
}
