using OpenAI_API.Models;
using TelegrabBotOpenAI.Models;

namespace TelegrabBotOpenAI.DataProviders
{
    public class CompletionSettingsEntity
    {
        public long Id { get; set; }
        public string LastPrompt { get; set; } = string.Empty;
        public Model Model { get; set; } = Model.DavinciText;
        public double Temerature { get; set; } = 0.3;
        public int MaxTokens { get; set; } = 3000;
        public double TopP { get; set; } = 1;
        public double FrequencyPenalty { get; set; } = 0;
        public double PresencePenalty { get; set; } = 0;

        public static CompletionSettingsEntity CreateEntity(CompletionSettings model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new CompletionSettingsEntity
            {
                Id = model.Id,
                LastPrompt = model.LastPrompt,
                Model = model.Model,
                Temerature = model.Temerature,
                MaxTokens = model.MaxTokens,
                TopP = model.TopP,
                FrequencyPenalty = model.FrequencyPenalty,
                PresencePenalty = model.PresencePenalty
            };
        }
    }
}
