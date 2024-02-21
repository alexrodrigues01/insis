using Microsoft.AspNetCore.Mvc;
using VotesGet.DTOs;
using VotesGet.Interfaces.Services;

namespace VotesGet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VoteController : Controller
{
    private IVoteService _service;

    public VoteController(IVoteService service)
    {
        _service = service;
    }

    [HttpGet("GetVoteByReview/{reviewID}")]
    public async Task<ActionResult<List<VoteReviewDTO>>> GetVoteByReview(long reviewID)
    {
        try
        {
            var voteResults = await _service.GetVoteByReview(reviewID);
            bool isSent;

            if(voteResults == null)
            {
                return NotFound();
            }
            else
            {
                 isSent = _service.SendMessageInformation(voteResults);   
            }

            return Ok(voteResults);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}