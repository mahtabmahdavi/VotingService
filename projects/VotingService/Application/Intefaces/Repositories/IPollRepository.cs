using SharedKernel.Entities;

namespace VotingService.Application.Intefaces.Repositories;

public interface IPollRepository
{
    Task<List<Poll>> GetAllPollsAsync();
    Task<Poll?> GetPollByIdAsync(Guid id);
    Task SubmitVoteAsync(Guid optionId, string voterId);
}
