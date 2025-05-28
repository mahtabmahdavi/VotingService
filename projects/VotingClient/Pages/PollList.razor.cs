using Grpc.Core;
using Microsoft.AspNetCore.Components;

using VotingService.Protos;

namespace VotingClient.Pages;

public partial class PollList : ComponentBase
{
    [Inject] protected VoteService.VoteServiceClient VoteClient { get; set; }
    [Inject] protected NavigationManager Navigation { get; set; }
    private List<Poll> Polls = new();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var response = await VoteClient.GetPollsAsync(new Empty());
            Console.WriteLine($"Poll count from server: {response.Polls.Count}");
            Polls = response.Polls.ToList();

        }
        catch (RpcException ex)
        {
            Console.Error.WriteLine($"gRPC error: {ex.Status.Detail}");
        }
    }

    private void NavigateToPoll(string pollId)
    {
        Navigation.NavigateTo($"/poll/{pollId}");
    }
}
