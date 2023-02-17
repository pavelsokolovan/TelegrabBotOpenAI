using Microsoft.EntityFrameworkCore;

namespace TelegrabBotOpenAI.DataProviders
{
    public abstract class BaseDataContext : DbContext, IDataContext
    {
        public DbSet<CompletionSettingsEntity> CompletionSettingsEntities { get; set; }
    }
}
