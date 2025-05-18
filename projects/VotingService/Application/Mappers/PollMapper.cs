using SharedKernel.Entities;
using VotingService.Application.Dtos;

namespace VotingService.Application.Mappers;

public static class PollMapper
{
    public static PollDto ToDto(Poll poll)
    {
        return new PollDto
        {
            Id = poll.Id,
            Title = poll.Title,
            Description = poll.Description,
            Questions = poll.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Text = q.Text,
                Options = q.Options.Select(o => new OptionDto
                {
                    Id = o.Id,
                    Text = o.Text,
                    VoteCount = o.Votes.Count
                }).ToList()
            }).ToList()
        };
    }
}
