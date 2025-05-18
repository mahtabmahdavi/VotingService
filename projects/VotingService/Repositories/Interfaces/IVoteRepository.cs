using SharedKernel.Entities;

namespace VotingService.Repositories.Interfaces;

public interface IVoteRepository
{
    Task<IEnumerable<Poll>> GetAllPollsAsync();
    Task<Poll?> GetPollByIdAsync(Guid id);
    Task<bool> SubmitVoteAsync(Guid optionId, string voterId);
}
