using Microsoft.EntityFrameworkCore;

namespace TelegrabBotOpenAI.DataProviders
{
    public interface IDataContext
    {
        DbSet<CompletionSettingsEntity> CompletionSettingsEntities { get; set; }
        int SaveChanges();
    }
}