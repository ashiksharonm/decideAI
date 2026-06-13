using DecideAI.Core.Interfaces;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using DecideAI.API.Plugins;

namespace DecideAI.API.Services;

public class CopilotService : ICopilotService
{
    private readonly Kernel _kernel;
    private readonly IChatCompletionService _chatCompletionService;

    public CopilotService(PortfolioDataPlugin portfolioPlugin)
    {
        var builder = Kernel.CreateBuilder();
        
        // We use Groq's OpenAI-compatible endpoint for free, fast cloud AI
        var groqApiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY") ?? "MISSING_KEY";
        
        builder.AddOpenAIChatCompletion(
            modelId: "llama3-8b-8192", 
            apiKey: groqApiKey,
            httpClient: new HttpClient() { BaseAddress = new Uri("https://api.groq.com/openai/v1/") }
        );

        _kernel = builder.Build();

        // Inject our custom Tools (MongoDB & Yahoo Finance)
        _kernel.Plugins.AddFromObject(portfolioPlugin, "PortfolioData");

        _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
    }

    public async Task<string> AskQuestionAsync(string question)
    {
        var history = new ChatHistory();
        history.AddSystemMessage("You are DecideAI, an Enterprise Portfolio Copilot. You help product portfolio managers analyze semiconductor products and financial data. You MUST use your provided tools to query the database and fetch live financial data to answer questions. Be concise and professional.");
        history.AddUserMessage(question);

        var executionSettings = new OpenAIPromptExecutionSettings 
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };

        try 
        {
            var result = await _chatCompletionService.GetChatMessageContentAsync(
                history,
                executionSettings: executionSettings,
                kernel: _kernel);

            return result.Content ?? "I couldn't generate a response.";
        }
        catch (Exception ex)
        {
            return $"Error connecting to Cloud LLM: {ex.Message}. Make sure you have set the GROQ_API_KEY environment variable in Render.";
        }
    }
}
