using TelegrabBotOpenAI.DataProviders;
using TelegrabBotOpenAI.Models;

namespace TelegrabBotOpenAI
{
    public interface IOpenAI
    {
        Task<string> GetComplitionAsync(string prompt, CompletionSettings settings);
    }
}