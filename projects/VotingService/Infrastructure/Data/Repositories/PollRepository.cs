using Microsoft.EntityFrameworkCore;
using SharedKernel.Entities;
using VotingService.Application.Intefaces.Repositories;

namespace VotingService.Infrastructure.Data.Repositories;

public class PollRepository : IPollRepository
{
    private readonly VotingDbContext _context;

    public PollRepository(VotingDbContext context)
    {
        _context = context;
    }

    public async Task<List<Poll>> GetAllPollsAsync()
    {
        return await _context.Polls
            .Include(p => p.Questions)
                .ThenInclude(q => q.Options)
                    .ThenInclude(o => o.Votes)
            .ToListAsync();
    }

    public async Task<Poll?> GetPollByIdAsync(Guid id)
    {
        return await _context.Polls
            .Include(p => p.Questions)
                .ThenInclude(q => q.Options)
                    .ThenInclude(o => o.Votes)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task SubmitVoteAsync(Guid optionId, string voterId)
    {
        var vote = new Vote
        {
            Id = Guid.NewGuid(),
            OptionId = optionId,
            VoterId = voterId,
            VotedAt = DateTime.UtcNow
        };

        _context.Votes.Add(vote);
        await _context.SaveChangesAsync();
    }
}
