using Google.Protobuf.WellKnownTypes;
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
}

