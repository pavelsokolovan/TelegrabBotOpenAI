using TelegrabBotOpenAI.Models;

namespace TelegrabBotOpenAI.DataProviders
{
    public interface IDataProvider
    {
        CompletionSettingsEntity ReadCompletionSettings(long id);
        void SaveCompletionSettings(CompletionSettingsEntity entity);
    }
}