using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Votes.Helpers;
using VotesApp.DTOs;
using VotesApp.Interfaces.Repository;
using VotesApp.Interfaces.Services;
using VotesApp.Model;

namespace VotesApp.Repositories;

public class VoteRepository : IVoteRepository
{
    private DataContext _context;

    private readonly IMapper _mapper;

    public VoteRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Boolean> FindVote(VoteReviewDTO voteReviewDTO)
    {
        if (_context.Votes.Where(x => x.reviewId == voteReviewDTO.reviewId && x.userId == voteReviewDTO.userId).Count() > 0)
        {
            return false;
        }

        return true;
    }

    public async Task CreateVote(Vote vote)
    {
        _context.Votes.Add(vote);
        _context.SaveChanges();
    }
}