
namespace TelegrabBotOpenAI.DataProviders
{
    public class DataProvider : IDataProvider
    {
        private readonly IDataContext dataContext;

        public DataProvider(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public CompletionSettingsEntity ReadCompletionSettings(long id)
        {
            return dataContext.CompletionSettingsEntities.Find(id) ??
                new CompletionSettingsEntity();
        }

        public void SaveCompletionSettings(CompletionSettingsEntity entity)
        {
            dataContext.CompletionSettingsEntities.Add(entity);
            dataContext.SaveChanges();
        }
    }
}