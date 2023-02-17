using OpenAI_API.Models;
using TelegrabBotOpenAI.DataProviders;

namespace TelegrabBotOpenAI.Models
{
    public class CompletionSettings
    {
        public long Id { get; set; }
        public string LastPrompt { get; set; } = string.Empty;
        public Model Model { get; set; } = Model.DavinciText;
        public double Temerature { get; set; }
        public int MaxTokens { get; set; }
        public double TopP { get; set; }
        public double FrequencyPenalty { get; set; }
        public double PresencePenalty { get; set; }

        public static CompletionSettings CreateModel(CompletionSettingsEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return new CompletionSettings
            {
                Id = entity.Id,
                LastPrompt = entity.LastPrompt,
                Model = entity.Model,
                Temerature = entity.Temerature,
                MaxTokens = entity.MaxTokens,
                TopP = entity.TopP,
                FrequencyPenalty = entity.FrequencyPenalty,
                PresencePenalty = entity.PresencePenalty
            };
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
