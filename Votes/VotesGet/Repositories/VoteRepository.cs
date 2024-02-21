using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VotesGet.Helpers;
using VotesGet.DTOs;
using VotesGet.Interfaces.Repository;
using VotesGet.Interfaces.Services;
using VotesGet.Model;

namespace VotesGet.Repositories;

public class VoteRepository : IVoteRepository
{
    private DataContext _context;

    private readonly IMapper _mapper;

    public VoteRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<VoteReviewDTO>> GetVoteByReview(long reviewID)
    {
        List<VoteReviewDTO> listVoteReviewDTO;

        var vote = await GetVoteByReviewID(reviewID);
        listVoteReviewDTO = _mapper.Map<List<VoteReviewDTO>>(vote);

        return listVoteReviewDTO;
    }

    private async Task<List<Vote>> GetVoteByReviewID(long reviewID)
    {
        return await _context.Votes.Where(x => x.reviewId == reviewID).ToListAsync();
    }

    public async Task CreateVote(Vote vote)
    {
        _context.Votes.Add(vote);
        _context.SaveChanges();
    }
}