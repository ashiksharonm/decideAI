using Microsoft.SemanticKernel;
using System;
using System.Net.Http;

class Program {
    static void Main() {
        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion(
            modelId: "llama3-8b-8192",
            apiKey: "dummy",
            httpClient: new HttpClient() { BaseAddress = new Uri("https://api.groq.com/openai/v1/") }
        );
    }
}
