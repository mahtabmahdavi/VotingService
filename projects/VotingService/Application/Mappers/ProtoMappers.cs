using SharedKernel.Entities;
using Poll = SharedKernel.Entities.Poll;

namespace VotingService.Application.Mappers;

public static class ProtoMapper
{
    public static Poll ToEntity(this Protos.Poll proto)
    {
        var poll = new Poll
        {
            Id = Guid.Parse(proto.Id),
            Title = proto.Title,
            Description = proto.Description,
            Questions = proto.Questions.Select(q => new Question
            {
                Id = Guid.Parse(q.Id),
                Text = q.Text,
                Options = q.Options.Select(o => new Option
                {
                    Id = Guid.Parse(o.Id),
                    Text = o.Text,
                    Votes = Enumerable.Range(0, o.VoteCount).Select(_ => new Vote
                    {
                        Id = Guid.NewGuid()
                    }).ToList()
                }).ToList()
            }).ToList()
        };

        foreach (var question in poll.Questions)
        {
            question.PollId = poll.Id;

            foreach (var option in question.Options)
            {
                option.QuestionId = question.Id;

                foreach (var vote in option.Votes)
                {
                    vote.OptionId = option.Id;
                }
            }
        }

        return poll;
    }

    public static Protos.Poll ToProto(this Poll poll)
    {
        return new Protos.Poll
        {
            Id = poll.Id.ToString(),
            Title = poll.Title,
            Description = poll.Description,
            Questions = { poll.Questions.Select(q => new Protos.Question
            {
                Id = q.Id.ToString(),
                Text = q.Text,
                Options = { q.Options.Select(o => new Protos.Option
                {
                    Id = o.Id.ToString(),
                    Text = o.Text,
                    VoteCount = o.Votes.Count
                })}
            })}
        };
    }
}
