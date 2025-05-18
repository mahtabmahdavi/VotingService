using Grpc.Core;
using VotingService.Application.Intefaces.Repositories;
using VotingService.Application.Mappers;
using VotingService.Protos;

namespace VotingService.Grpc.Services;

public class VoteGrpcService : VoteService.VoteServiceBase
{
    private readonly IPollRepository _repository;
    private readonly ILogger<VoteGrpcService> _logger;

    public VoteGrpcService(IPollRepository repository, ILogger<VoteGrpcService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public override async Task<PollListResponse> GetPolls(Protos.Empty request, ServerCallContext context)
    {
        var polls = await _repository.GetAllPollsAsync();
        var response = new PollListResponse();
        response.Polls.AddRange(polls.Select(p => p.ToProto()));
        return response;
    }

    public override async Task<PollResponse> GetPollById(GetPollByIdRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.PollId, out var id))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid PollId"));

        var poll = await _repository.GetPollByIdAsync(id);

        if (poll == null)
            throw new RpcException(new Status(StatusCode.NotFound, "Poll not found"));

        return new PollResponse { Poll = poll.ToProto() };
    }
}

