namespace Votes.Interfaces.Model
{
    public interface IVote
    {
        public string vote { get; set; }

        public long userId { get; set; }

        public long reviewId { get; set; }
    }
}
