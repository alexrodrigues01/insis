using Microsoft.AspNetCore.Mvc;
using VotesApp.DTOs;
using VotesApp.Interfaces.Services;

namespace VotesApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VoteController : Controller
{
    private IVoteService _service;

    public VoteController(IVoteService service)
    {
        _service = service;
    }

    [HttpPost("CreateVote")]
    public async Task<ActionResult<VoteReviewDTO>> CreateVote(VoteReviewDTO vote)
    {
        try
        {
            var voteResults = await _service.CreateVote(vote);
            bool isSent = false;
            if(vote != null)
            {
                isSent = _service.SendVote(vote);
                isSent = _service.SendMessageInformation(vote);
            }
            else
            {
                throw new System.ArgumentNullException("Body is null");
            }
            return Ok(voteResults);
        }
        catch (Exception ex)
        {
            return BadRequest(new {Message = ex.Message});
        }
    }
}