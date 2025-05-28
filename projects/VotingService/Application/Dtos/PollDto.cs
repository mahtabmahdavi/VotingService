namespace VotingService.Application.Dtos;

public class PollDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<QuestionDto> Questions { get; set; } = new();
}
