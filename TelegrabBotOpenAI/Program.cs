using Autofac;
using TelegrabBotOpenAI;

var container = ContainerConfig.Configure();
using var scope = container.BeginLifetimeScope();
var application = scope.Resolve<IApplication>();
application.Run();
