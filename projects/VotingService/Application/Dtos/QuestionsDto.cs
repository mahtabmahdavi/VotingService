namespace VotingService.Application.Dtos;

public class QuestionDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public List<OptionDto> Options { get; set; } = new();
}
