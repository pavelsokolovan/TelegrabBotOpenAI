using OpenAI_API;
using OpenAI_API.Completions;
using TelegrabBotOpenAI.Models;

namespace TelegrabBotOpenAI
{
    public class OpenAI : IOpenAI
    {
        private readonly OpenAIAPI api;

        public OpenAI(ISettings settings)
        {
            this.api = new OpenAIAPI(new APIAuthentication(settings.OpenAIAPIToken));
        }

        public async Task<string> GetComplitionAsync(string prompt, CompletionSettings settings)
        {
            var complitionRequest = new CompletionRequest(
                prompt: prompt,
                model: settings.Model,
                temperature: settings.Temerature,
                max_tokens: settings.MaxTokens,
                top_p: settings.TopP,
                frequencyPenalty: settings.FrequencyPenalty,
                presencePenalty: settings.PresencePenalty
            );

            var result = await api.Completions.CreateCompletionAsync(complitionRequest);

            return result.ToString();
        }
    }
}
