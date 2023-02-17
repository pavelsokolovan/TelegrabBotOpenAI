using Autofac;
using Microsoft.Extensions.Configuration;
using Serilog;
using TelegrabBotOpenAI.DataProviders;

namespace TelegrabBotOpenAI
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Settings>().As<ISettings>();
            builder.RegisterType<OpenAI>().As<IOpenAI>();
            builder.RegisterType<Bot>().As<IBot>();
            builder.RegisterType<SqliteDataContext>().As<IDataContext>();
            builder.RegisterType<DataProvider>().As<IDataProvider>();
            builder.RegisterType<Application>().As<IApplication>();

            // register configuration from appsettings.json
            builder.Register<IConfiguration>(c => new ConfigurationBuilder()
                                                    .AddJsonFile("appsettings.json")
                                                    .Build());
            // register logger
            builder.Register<ILogger>(c => 
                            new LoggerConfiguration()
                            .ReadFrom.Configuration(c.Resolve<IConfiguration>())
                            .CreateLogger())
                    .SingleInstance();

            return builder.Build();
        }
    }
}
