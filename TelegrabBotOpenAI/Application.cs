using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace TelegrabBotOpenAI
{
    public class Application : IApplication
    {
        private readonly IBot bot;
        private readonly ILogger logger;

        public Application(IBot bot, ILogger logger)
        {
            this.bot = bot;
            this.logger = logger;
        }

        public void Run()
        {
            try
            {
                bot.Start();
                logger.Information("bot is started!");
                while (true) { }
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                throw;
            }
            finally
            {
                bot.Stop();
                logger.Information("bot is stopped!");
            }
        }
    }
}
