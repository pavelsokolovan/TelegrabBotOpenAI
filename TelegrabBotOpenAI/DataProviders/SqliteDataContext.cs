using Microsoft.EntityFrameworkCore;

namespace TelegrabBotOpenAI.DataProviders
{
    public class SqliteDataContext : BaseDataContext
    {
        public SqliteDataContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=BotDb.db");
        }
    }
}
