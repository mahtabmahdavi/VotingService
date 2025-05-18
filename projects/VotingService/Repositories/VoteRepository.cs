using Microsoft.EntityFrameworkCore;
using SharedKernel.Entities;
using VotingService.Persistence.Context;
using VotingService.Repositories.Interfaces;

namespace VotingService.Repositories;

public class VoteRepository : IVoteRepository
{
    private readonly VotingDbContext _context;

    public VoteRepository(VotingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Poll>> GetAllPollsAsync()
    {
        return await _context.Polls
            .Include(p => p.Questions)
                .ThenInclude(q => q.Options)
            .ToListAsync();
    }

    public async Task<Poll?> GetPollByIdAsync(Guid id)
    {
        return await _context.Polls
            .Include(p => p.Questions)
                .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> SubmitVoteAsync(Guid optionId, string voterId)
    {
        var optionExists = await _context.Options.AnyAsync(o => o.Id == optionId);
        if (!optionExists) return false;

        var vote = new Vote
        {
            Id = Guid.NewGuid(),
            OptionId = optionId,
            VoterId = voterId,
            VotedAt = DateTime.UtcNow
        };

        _context.Votes.Add(vote);
        await _context.SaveChangesAsync();
        return true;
    }
}
