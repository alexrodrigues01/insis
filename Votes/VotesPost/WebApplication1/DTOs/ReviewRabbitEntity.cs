namespace Votes.DTOs
{
    public class ReviewRabbitEntity
    {

        public long idReview { get; set; }
        public string reviewText { get; set; }
        public string publishingDate { get; set; }
        public string approvalStatus { get; set; }
        public string funFact { get; set; }
        public double ? rating { get; set; }
        private long ? userID { get; set; }
        private string sku { get; set; }

    }
}
