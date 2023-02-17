using Microsoft.Extensions.Configuration;

namespace TelegrabBotOpenAI
{
    public class Settings : ISettings
    {
        private const string TELEGRAM_BOT_TOKEN = "TELEGRAM_BOT_TOKEN";
        private const string OPENAI_API_TOKEN = "OPENAI_API_TOKEN";

        public string BotToken { get; }
        public string OpenAIAPIToken { get; }

        public Settings()
        {
            BotToken = Environment.GetEnvironmentVariable(TELEGRAM_BOT_TOKEN) ?? String.Empty;
            OpenAIAPIToken = Environment.GetEnvironmentVariable(OPENAI_API_TOKEN) ?? String.Empty;
            ValidateVariable(TELEGRAM_BOT_TOKEN, BotToken);
            ValidateVariable(OPENAI_API_TOKEN, OpenAIAPIToken);
        }

        private void ValidateVariable(string variableName, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException($"{variableName} environmant variable is not set");
            }
        }
    }
}