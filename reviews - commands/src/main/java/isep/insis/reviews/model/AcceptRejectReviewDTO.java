package isep.insis.reviews.model;

import java.io.Serializable;

public class AcceptRejectReviewDTO implements Serializable {

    private final Long reviewId;
    private final String approved;
    public AcceptRejectReviewDTO(Long reviewID, String approved) {
        this.reviewId = reviewID;
        this.approved = approved;
    }

    public Long getReviewId() {
        return reviewId;
    }

    public String getApproved() {
        return approved;
    }
}
