namespace DecideAI.Core.Interfaces;

public interface ICopilotService
{
    Task<string> AskQuestionAsync(string question);
}
