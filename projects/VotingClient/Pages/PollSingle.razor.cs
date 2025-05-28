namespace VotingClient.Pages;

using Grpc.Core;
using Microsoft.AspNetCore.Components;
using VotingService.Protos;

public partial class PollSingle : ComponentBase
{
    [Parameter] public string PollId { get; set; } = string.Empty;

    [Inject] protected VoteService.VoteServiceClient VoteClient { get; set; }
    [Inject] protected NavigationManager Navigation { get; set; }

    protected Poll? CurrentPoll;
    protected Dictionary<string, string> SelectedOptionIds = new();
    protected bool IsLoading = true;

    private string VoterId = Guid.NewGuid().ToString();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var request = new GetPollByIdRequest { PollId = PollId };
            var response = await VoteClient.GetPollByIdAsync(request);
            CurrentPoll = response.Poll;
        }
        catch (RpcException ex)
        {
            Console.Error.WriteLine($"Error fetching poll: {ex.Status.Detail}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    protected void OnOptionSelected(string questionId, string optionId)
    {
        SelectedOptionIds[questionId] = optionId;
    }

    protected bool AllQuestionsAnswered =>
        CurrentPoll != null &&
        CurrentPoll.Questions.All(q => SelectedOptionIds.ContainsKey(q.Id));

    protected async Task SubmitVote()
    {
        if (CurrentPoll is null || !AllQuestionsAnswered)
            return;

        try
        {
            foreach (var (questionId, optionId) in SelectedOptionIds)
            {
                var voteRequest = new VoteRequest
                {
                    OptionId = optionId,
                    VoterId = VoterId
                };

                await VoteClient.SubmitVoteAsync(voteRequest);
            }

            Navigation.NavigateTo("/poll-list");
        }
        catch (RpcException ex)
        {
            Console.Error.WriteLine($"Vote submission failed: {ex.Status.Detail}");
        }
    }
}
