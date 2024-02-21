using VotesGet.Helpers;
using VotesGet.Model;

namespace VotesGet.Repositories
{
    public class VoteBootstrap : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public VoteBootstrap(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {



            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();


                dbContext.Database.EnsureCreated();

                Vote review1Vote1 = new Vote("downVote",30,1);
                Vote review1Vote2 = new Vote("upVote", 31, 1);
                Vote review1Vote3 = new Vote("upVote", 32, 1);
                Vote review2Vote1 = new Vote("downVote", 30, 2);
                Vote review2Vote2 = new Vote("upVote", 31, 2);
                Vote review2Vote3 = new Vote("upVote", 32, 2);
                Vote review3Vote1 = new Vote("downVote", 30, 3);
                Vote review3Vote2 = new Vote("upVote", 31, 3);
                Vote review3Vote3 = new Vote("upVote", 32, 3);
                Vote review4Vote1 = new Vote("downVote", 30, 4);
                Vote review4Vote2 = new Vote("upVote", 31, 4);
                Vote review4Vote3 = new Vote("upVote", 32, 4);
                Vote review5Vote1 = new Vote("downVote", 30, 5);
                Vote review5Vote2 = new Vote("upVote", 31, 5);
                Vote review5Vote3 = new Vote("upVote", 32, 5);
                Vote review6Vote1 = new Vote("downVote", 30, 6);
                Vote review6Vote2 = new Vote("downVote", 31, 6);
                Vote review6Vote3 = new Vote("upVote", 32, 6);
                Vote review7Vote1 = new Vote("downVote", 30, 7);
                Vote review7Vote2 = new Vote("downVote", 31, 7);
                Vote review7Vote3 = new Vote("upVote", 32, 7);
                Vote review8Vote1 = new Vote("downVote", 30, 8);
                Vote review8Vote2 = new Vote("downVote", 31, 8);
                Vote review8Vote3 = new Vote("upVote", 32, 8);
                Vote review9Vote1 = new Vote("downVote", 30, 9);
                Vote review9Vote2 = new Vote("downVote", 31, 9);
                Vote review9Vote3 = new Vote("upVote", 32, 9);
                Vote review10Vote1 = new Vote("downVote", 30, 10);
                Vote review10Vote2 = new Vote("downVote", 31, 10);
                Vote review10Vote3 = new Vote("upVote", 32, 10);
                Vote review11Vote1 = new Vote("downVote", 30, 11);
                Vote review11Vote2 = new Vote("downVote", 31, 11);
                Vote review11Vote3 = new Vote("upVote", 32, 11);
                Vote review12Vote1 = new Vote("downVote", 30, 12);
                Vote review12Vote2 = new Vote("downVote", 31, 12);
                Vote review12Vote3 = new Vote("upVote", 32, 12);


                dbContext.Votes.AddRange(review1Vote1, review1Vote2, review1Vote3, review2Vote1, review2Vote2, review2Vote3,
                    review3Vote1, review3Vote2, review3Vote3, review4Vote1, review4Vote2, review4Vote3, review5Vote1, review5Vote2, review5Vote3,
                    review6Vote1, review6Vote2, review6Vote3, review7Vote1, review7Vote2, review7Vote3, review8Vote1, review8Vote2, review8Vote3,
                    review9Vote1, review9Vote2, review9Vote3, review10Vote1, review10Vote2, review10Vote3, review11Vote1, review11Vote2, review11Vote3,
                    review12Vote1, review12Vote2, review12Vote3);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
